using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GrandpaStates;

public class GrandpaMovement : State<Grandpa>
{
    private static GrandpaMovement _instance;

    public GameObject target;
    public bool isMoving;
    public bool atTarget;
    public bool onEndzone;
    public bool itemCollision;
    public float randomNumber;
    private float speed;

    private GrandpaMovement()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
        isMoving = false;
        atTarget = false;
        onEndzone = false;
        itemCollision = false;
        speed = 2f;

    }

    public static GrandpaMovement Instance
    {
        get
        {
            if (_instance == null)
            {
                new GrandpaMovement();
            }
            return _instance;
        }
    }

    public override void EnterState(Grandpa _owner)
    {
        Debug.Log("Entering GrandpaMovement");

        // if(_owner.grandpaModel.GetComponent<Animator>().)

        _owner.grandpaModel.GetComponent<Animator>().SetTrigger("IdleToWalk"); //walk animation startet
        _owner.audios[1].Play();

        randomNumber = Random.Range(0.0f, 1.0f);
        Debug.Log("rnd number: " + randomNumber);
        SetTarget(_owner);
        isMoving = true;
        Rotate(_owner, target);
    }

    public override void ExitState(Grandpa _owner)
    {
        Debug.Log("Exit GrandpaMovement");
        //_owner.grandpaModel.GetComponent<Animator>().SetTrigger("WalkToIdle");

        _owner.grandpaModel.GetComponent<Animator>().ResetTrigger("IdleToWalk"); // walk animation endet

        _owner.audios[1].Stop();

        if (atTarget)
        {
            target = null;
            atTarget = false;
        }

        if(GrandpaDetection.Instance.endPlane != null && !GrandpaDetection.Instance.endPlaneInSight)
        {
            Debug.Log("grandpa muss sich drehen");
            _owner.transform.rotation = GrandpaDetection.Instance.endPlane.transform.rotation;
        }
    }

    public override void UpdateState(Grandpa _owner)
    {

        //if (!_owner.switchState)
        //{
        //    _owner.stateMachine.ChangeState(GrandpaDetection.Instance);
        //}

        if (_owner.state == 0)
        {
            _owner.stateMachine.ChangeState(GrandpaDetection.Instance);
        }

        if(_owner.state == 2)
        {
            _owner.stateMachine.ChangeState(GrandpaPickup.Instance);
        }

        /*
        if (!isMoving)
        {
            _owner.stateMachine.ChangeState(GrandpaDetection.Instance);
        }
        */
        //Rotate(_owner, target);
        if (_owner != null)
        {
            Move(_owner, target);
        }
        //Move(_owner, target);
        //Debug.Log("Update GrandpaMovement");
    }

    public void Rotate(Grandpa _owner, GameObject target)
    {
        if (target != null)
        {
            var distance = Vector3.Distance(_owner.transform.position, target.transform.position);
            var targetPos = target.transform.position;
            targetPos.y = _owner.transform.position.y;

            var relativePos = targetPos - _owner.transform.position;
            var rotation = Quaternion.LookRotation(relativePos);
            _owner.grandpaModel.transform.rotation = rotation;
        }
    }

    public void Move(Grandpa _owner, GameObject target)
    {
        if (target != null)
        {
            var distance = Vector3.Distance(_owner.transform.position, target.transform.position);
            var targetPos = target.transform.position;
            targetPos.y = _owner.transform.position.y;

            var relativePos = targetPos - _owner.transform.position;
            var rotation = Quaternion.LookRotation(relativePos);

            isMoving = !isMoving;
            //Debug.Log("in move()");

            //Debug.Log("in move() mit target");
            if (_owner.transform.position != targetPos)
            {
                //Debug.Log("move ausführen");

                //_owner.transform.position = Vector3.MoveTowards(_owner.transform.position, target.transform.position, Time.deltaTime * speed);

                //_owner.grandpaModel.transform.rotation = rotation;

                //_owner.transform.rotation = rotation;
                _owner.transform.position = Vector3.MoveTowards(_owner.transform.position, targetPos, Time.deltaTime * speed);
            }
            else
            {
                if (target.GetComponent<Item>())
                {
                    GrandpaDetection.Instance.items.Remove(target.GetComponent<Item>());
                    _owner.removeFromDetection(target);
                    target.SetActive(false);
                }
                if (target.GetComponent<PlaneChecking>())
                {
                    if (target.GetComponent<PlaneChecking>().isEnd)
                    {
                        onEndzone = true;
                        Debug.Log("Player is onEndzone: " + onEndzone);
                        GameObject.FindGameObjectWithTag("GameController").SendMessage("GrandpaGoal");
                    }
                }
                atTarget = true;
            }
        }
    }

    public bool IsAtTarget(Grandpa _owner)
    {
        if (_owner.transform.position == target.transform.position)
        {
            return true;
        }
        return false;
    }

    public void SetTarget(Grandpa _owner)
    {
        var detection = GrandpaDetection.Instance;

        if (target == null)
        {

            if (GrandpaDetection.Instance.endPlane != null)
            {
                var localTarget = detection.freePlanes[0];

                var endPlane = GrandpaDetection.Instance.endPlane;

                foreach (var plane in detection.freePlanes)
                {
                    var distAct = Vector3.Distance(localTarget.transform.position, endPlane.transform.position);
                    var distNew = Vector3.Distance(plane.transform.position, endPlane.transform.position);

                    if (distNew < distAct)
                    {
                        localTarget = plane;
                    }
                }

                target = localTarget;

            }
            else if (detection.items.Count != 0)
            {
                var localTarget = detection.freePlanes[0];
                foreach (var plane in detection.freePlanes)
                {
                    var distAct = Vector3.Distance(localTarget.transform.position, detection.items[0].transform.position);
                    var distNew = Vector3.Distance(plane.transform.position, detection.items[0].transform.position);

                    if (distNew < distAct)
                    {
                        localTarget = plane;
                    }
                }

                target = localTarget;
                Debug.Log("set item target");
            }
            else
            {

                var factor = detection.freePlanes.Count;
                Debug.Log(factor);

                switch (factor)
                {
                    case 0:
                        if (randomNumber > 0.5f)
                        {
                            _owner.transform.Rotate(0, 90, 0, Space.World);
                            Debug.Log("rotate right");
                        }
                        else
                        {
                            _owner.transform.Rotate(0, -90, 0, Space.World);
                            Debug.Log("rotate left");
                        }
                        Debug.Log("set freePlane target case 0");
                        atTarget = true;
                        break;
                    case 1:
                        target = detection.freePlanes[0];
                        Debug.Log("set freePlane target case 1");
                        break;

                    case 2:
                        if (randomNumber > 0.5f)
                        {
                            target = detection.freePlanes[0];
                            Debug.Log("set freePlane target case 2");
                        }
                        else
                        {
                            target = detection.freePlanes[1];
                            Debug.Log("set freePlane target case 2");
                        }
                        break;

                    case 3:
                        if (randomNumber > 0.66f)
                        {
                            target = detection.freePlanes[0];
                            Debug.Log("set freePlane target case 3");
                        }
                        else if (randomNumber <= 0.66f && randomNumber > 0.33f)
                        {
                            target = detection.freePlanes[1];
                            Debug.Log("set freePlane target case 3");
                        }
                        else
                        {
                            target = detection.freePlanes[2];
                            Debug.Log("set freePlane target case 3");
                        }
                        break;

                    default:
                        Debug.Log("set freePlane went wrong");
                        break;

                }

            }
        }
    }
}
