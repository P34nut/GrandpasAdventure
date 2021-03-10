using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GrandpaStates;

public class Grandpa : MonoBehaviour {

    public bool switchState = false;
    public float gameTimer;
    public int seconds = 0;

    public int state = 0;

    public GameObject orientationLeft;
    public GameObject orientationMid;
    public GameObject orientationRight;
    public GameObject gameOverScreen;

    public GameObject target;
    public GameObject endPlane;

    public bool itemCollision;
    public bool isDetecting;
    public bool atTarget;
    public bool isPickedUp;

    public GameObject grandpaModel;

    public AudioSource[] audios;

    public List<GameObject> floorSensors;

    public List<GameObject> surroundings;
    public List<GameObject> freePlanes;
    public List<Item> items;

    public TestZustandPlayer feeling;

    public GrandpaStateMachine<Grandpa> stateMachine { get; set; }


    private void Awake()
    {
        stateMachine = new GrandpaStateMachine<Grandpa>(this);
        stateMachine.ChangeState(GrandpaDetection.Instance);
        gameTimer = Time.time;

        feeling = GetComponent<TestZustandPlayer>();
        audios = grandpaModel.GetComponents<AudioSource>();
    }

    private void Update()
    {
        if(Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            //Debug.Log(seconds);
        }
        if(seconds == 1)
        {
            seconds = 0;
            //switchState = !switchState;
        }



        if(GrandpaMovement.Instance.itemCollision == true || GrandpaDetection.Instance.isDetecting == false)
        {
            //switchState = !switchState;
            state++;
        }
        if (GrandpaMovement.Instance.atTarget == true || GrandpaPickup.Instance.isPickedUp == true)
        {
            //switchState = !switchState;
            state--;
        }


        freePlanes = GrandpaDetection.Instance.freePlanes;
        items = GrandpaDetection.Instance.items;
        target = GrandpaMovement.Instance.target;

        itemCollision = GrandpaMovement.Instance.itemCollision;
        isDetecting = GrandpaDetection.Instance.isDetecting;
        atTarget = GrandpaMovement.Instance.atTarget;
        isPickedUp = GrandpaPickup.Instance.isPickedUp;

        endPlane = GrandpaDetection.Instance.endPlane;

        //Debug.Log(GrandpaPickup.Instance);

        // Debug.Log("picked up: " + GrandpaPickup.Instance.isPickedUp);
        //Debug.Log(stateMachine.currentState);

        stateMachine.Update();

    }

        public void removeFromDetection(GameObject obj)
    {
        //Debug.Log("item removed: " + obj.name);
        surroundings.Remove(obj);

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Grandpa collision: " + other.name);
        //if (other.tag == "Consumable")
        //{
        //    Debug.Log("set itemCollision");
        //    GrandpaMovement.Instance.itemCollision = true;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Consumable")
        {
            Debug.Log("item collision");
            GrandpaMovement.Instance.itemCollision = true;
            GrandpaDetection.Instance.collidedItem = collision.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //removeFromDetection(other.gameObject);
    }

    void PlayerIsDead()
    {
        //playerControllerScript.enabled = false;
        //itemPickupScript.enabled = false;
        //GetComponentInChildren<ItemPickup>().enabled = false;

        GetComponent<Grandpa>().enabled = false;
        grandpaModel.GetComponent<Animator>().SetTrigger("Death");
        StartCoroutine(waitForDeath());
        audios[1].Stop();
        GetComponent<AudioSource>().Play();
        Debug.Log("Grandpa ist gestorben");
    }

    private IEnumerator waitForDeath()
    {
        yield return new WaitForSeconds(4f);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        StopCoroutine(waitForDeath());
    }

}
