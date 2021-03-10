using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneChecking : MonoBehaviour {

    public bool blocked = false;
    public bool walkable = true;
    public bool playerTouched = false;
    public bool isEnd = false;
    public bool isStart = false;
    public bool isDeactivated;

    private void Start()
    {
        if (transform.childCount > 1)
        {
            if (gameObject.transform.GetChild(1).tag == "Trap")
                IsBlocked();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        string tag = other.gameObject.tag;

        if (tag == "Barrier" || tag == "Handitem" || tag == "Consumable" || tag == "Trap")
        {
            blocked = true;
        }

        if (tag == "Barrier" || tag == "Handitem")
        {
            walkable = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.gameObject.tag;

        if (tag == "Barrier" || tag == "Handitem" || tag == "Consumable" || tag == "Trap")
            blocked = false;

        if (tag == "Handitem")
        {
            walkable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerRadius")
            playerTouched = true;

        string tag = other.gameObject.tag;

        if (tag == "Barrier" || tag == "Handitem" || tag == "Consumable" || tag == "Trap")
        {
            blocked = true;
        }
    }



    private void IsBlocked()
    {
        blocked = true;
    }

    private void IsNotBlocked()
    {
        blocked = false;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "CamRay")
            blocked = true;
    }*/

    private void CheckLight()
    {
        if (playerTouched)
        {
            Light light = GetComponentInChildren<Light>();
            light.enabled = !light.enabled;
        }
    }

    private void IsWalkable()
    {
        walkable = true;
    }
}
