using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GrandpaStates;

public class GrandpaDetection : State<Grandpa>
{
    private static GrandpaDetection _instance;

    public List<GameObject> surroundings;
    public List<Item> items;
    public List<GameObject> freePlanes;

    public GameObject collidedItem;

    public GameObject endPlane;

    public Vector3 grandpaHeadPos;

    public bool isDetecting;
    public bool endPlaneInSight;

    public float gameTimer;
    public int seconds = 0;

    private GrandpaDetection()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
        isDetecting = true;
        items = new List<Item>();
        freePlanes = new List<GameObject>();
    }

    public static GrandpaDetection Instance
    {
        get
        {
            if (_instance == null)
            {
                new GrandpaDetection();
            }
            return _instance;
        }
    }

    public override void EnterState(Grandpa _owner)
    {
        //isDetecting = true;
        grandpaHeadPos = new Vector3(_owner.transform.position.x, _owner.transform.GetComponent<Renderer>().bounds.size.y / 2, _owner.transform.position.z);

        freePlanes.Clear();

        _owner.orientationLeft.SetActive(true);
        _owner.orientationMid.SetActive(true);
        _owner.orientationRight.SetActive(true);

        _owner.grandpaModel.GetComponent<Animator>().SetTrigger("WalkToIdle");

        //checkForEnd(_owner);
        //getItemsInSight(_owner);

        Debug.Log(grandpaHeadPos);
        Debug.Log("Entering GrandpaDetection");
        //isDetecting = false;

    }

    public override void ExitState(Grandpa _owner)
    {
        /*
        Debug.Log(_owner.surroundings.Count);
        foreach(var item in _owner.surroundings){
            Debug.Log(item.name);
        }
        */

        _owner.orientationLeft.SetActive(false);
        _owner.orientationMid.SetActive(false);
        _owner.orientationRight.SetActive(false);

        isDetecting = true;

        checkForEnd(_owner);
        getItemsInSight(_owner);

        _owner.grandpaModel.GetComponent<Animator>().ResetTrigger("WalkToIdle");

        Debug.Log("Exit GrandpaDetection");
    }

    public override void UpdateState(Grandpa _owner)
    {
        //Debug.Log("Entering GrandpaDetection update");

        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }
        if (seconds == 3)
        {
            seconds = 0;
            isDetecting = false;
        }

        //if (_owner.switchState)
        //{
        //    _owner.stateMachine.ChangeState(GrandpaMovement.Instance);
        //}

        if(_owner.state == 1)
        {
            _owner.stateMachine.ChangeState(GrandpaMovement.Instance);
        }


        //Debug.Log("Update GrandpaDetection");
    }

    /*
    public void getSurroundings(Grandpa _owner){
        Collider[] hitColliders = Physics.OverlapBox(_owner.transform.localPosition + _owner.orientationMid.transform.localPosition, new Vector3(2.5f,1.0f,5.0f),Quaternion.identity);
        foreach(var collider in hitColliders){
            if(!_owner.surroundings.Contains(collider.gameObject)){
                if(collider.tag == "Floor" || collider.tag == "Trap" || collider.tag == "Consumable"){
                    _owner.surroundings.Add(collider.gameObject);
                }
            }
        }
    }
    */

    public void checkForEnd(Grandpa _owner)
    {
        var list = _owner.surroundings;
        foreach (var item in list)
        {
            if (item.GetComponent<PlaneChecking>())
            {
                if (item.GetComponent<PlaneChecking>().isEnd)
                {
                    endPlane = item;
                    endPlaneInSight = true;
                }
            }
        }
    }

    public void getItemsInSight(Grandpa _owner)
    {
        var list = _owner.surroundings;
        //Debug.Log("in getItemsInSight");

        foreach (var item in list)
        {
            if (item.tag == "Consumable")
            {
                Item actItem = item.GetComponent<Item>();

                RaycastHit hit0;
                RaycastHit hit1;
                RaycastHit hit2;
                RaycastHit hit3;
                RaycastHit hit4;

                //im Folgenden soll transform.position später durch grandpaHeadPos ersetzt werden
                var direction = actItem.transform.position - _owner.transform.position;

                var mainDir = grandpaHeadPos - actItem.transform.position;

                var layerMask = 1 << 12;
                layerMask = ~layerMask;

                //Debug.Log("Bounds size: " + actItem.GetComponent<Renderer>().bounds.size);

                //set Raycast positions and directions
                var posXlow = new Vector3(actItem.transform.position.x - (actItem.GetComponent<Renderer>().bounds.size.x / 2), actItem.transform.position.y + (actItem.GetComponent<Renderer>().bounds.size.y / 2), actItem.transform.position.z);
                var dirX1 = grandpaHeadPos - posXlow;

                var posXhigh = new Vector3(actItem.transform.position.x + (actItem.GetComponent<Renderer>().bounds.size.x / 2), actItem.transform.position.y + (actItem.GetComponent<Renderer>().bounds.size.y / 2), actItem.transform.position.z);
                var dirX2 = grandpaHeadPos - posXhigh;

                var posZlow = new Vector3(actItem.transform.position.x, actItem.transform.position.y + (actItem.GetComponent<Renderer>().bounds.size.y / 2), actItem.transform.position.z - (actItem.GetComponent<Renderer>().bounds.size.z / 2));
                var dirZ1 = grandpaHeadPos - posZlow;

                var posZhigh = new Vector3(actItem.transform.position.x, actItem.transform.position.y + (actItem.GetComponent<Renderer>().bounds.size.y / 2), actItem.transform.position.z + (actItem.GetComponent<Renderer>().bounds.size.z / 2));
                var dirZ2 = grandpaHeadPos - posZhigh;

                //Debug.DrawRay(transform.position, direction,Color.red);

                Debug.DrawRay(actItem.transform.position, mainDir, Color.green);
                Debug.DrawRay(posXlow, dirX1, Color.cyan);
                Debug.DrawRay(posXhigh, dirX2, Color.cyan);
                Debug.DrawRay(posZlow, dirZ1, Color.cyan);
                Debug.DrawRay(posZhigh, dirZ2, Color.cyan);


                if ((Physics.Raycast(actItem.transform.position, mainDir, out hit0) |
                    Physics.Raycast(posXlow, dirX1, out hit1) |
                    Physics.Raycast(posXhigh, dirX2, out hit2) |
                    Physics.Raycast(posZlow, dirZ1, out hit3) |
                    Physics.Raycast(posZhigh, dirZ2, out hit4)))
                {
                    /*
                    Debug.Log("hit0: " + hit0.collider.gameObject.name);
                    Debug.Log("hit1: " + hit1.collider.gameObject.name);
                    Debug.Log("hit2: " + hit2.collider.gameObject.name);
                    Debug.Log("hit3: " + hit3.collider.gameObject.name);
                    Debug.Log("hit4: " + hit4.collider.gameObject.name);
                    */

                    //Debug.Log("in getItemsInSight if1");
                    //hier müsste noch nach Collider-Art abgefragt werden.
                    //schönerer Weg?
                    // layer-mask erstellen!
                    if (hit0.collider.gameObject.name == "Grandpa" |
                        hit1.collider.gameObject.name == "Grandpa" |
                        hit2.collider.gameObject.name == "Grandpa" |
                        hit3.collider.gameObject.name == "Grandpa" |
                        hit4.collider.gameObject.name == "Grandpa")
                    {
                        //Debug.Log("in getItemsInSight if2");
                        if (!items.Contains(actItem))
                        {
                            
                            items.Add(actItem); //grandpa erkennt item!
                            _owner.audios[0].Play();
                        }
                        else
                        {
                            //Debug.Log(actItem.name + " ist bereits in Liste");
                        }

                    }
                    else
                    {
                        //Debug.Log("Remove: " + actItem.gameObject.name);
                        items.Remove(actItem);
                    }

                }
            }
        }
    }

    public List<Item> getItems()
    {
        return items;
    }

    public void resetFreePlanes()
    {
        freePlanes.Clear();
    }

    public void removeFromDetection(GameObject obj)
    {
        //Debug.Log("item removed: " + obj.name);
        //surroundings.Remove(obj);

        if (items.Contains(obj.GetComponent<Item>()))
        {
            items.Remove(obj.GetComponent<Item>());
        }

        if (freePlanes.Contains(obj))
        {
            freePlanes.Remove(obj);
        }
    }

}
