using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GrandpaStates;

public class GrandpaPickup : State<Grandpa>
{
    private static GrandpaPickup _instance;

    public bool isPickedUp;

    private GrandpaPickup()
    {
        if(_instance != null)
        {
            return;
        }

        _instance = this;
        isPickedUp = false;
    }

    public static GrandpaPickup Instance
    {
        get
        {
            if (_instance == null)
            {
                new GrandpaPickup();
            }
            return _instance;
        }
    }

    public override void EnterState(Grandpa _owner)
    {
        Debug.Log("Enter GrandpaPickup");

        _owner.grandpaModel.GetComponent<Animator>().SetTrigger("WalkToPickUp"); //pickup animation startet

        GrandpaMovement.Instance.itemCollision = false;

        var item = GrandpaDetection.Instance.collidedItem;
        GrandpaDetection.Instance.items.Remove(item.GetComponent<Item>());
        _owner.removeFromDetection(item);

        if (item.GetComponent<Item>().name == "Alcohol")
        {
            _owner.feeling.IncAlkohol();
        }
        if (item.GetComponent<Item>().name == "Greed")
        {
            _owner.feeling.IncGier();
        }
        if (item.GetComponent<Item>().name == "Fear")
        {
            _owner.feeling.IncAngst();
        }

        item.SetActive(false);
        //isPickedUp = true;

        //_owner.state--;

    }

    public override void ExitState(Grandpa _owner)
    {
        //_owner.grandpaModel.GetComponent<Animator>().SetTrigger("PickUpToWalk");

        _owner.grandpaModel.GetComponent<Animator>().ResetTrigger("WalkToPickUp"); //pickup animation endet
        _owner.grandpaModel.GetComponent<Animator>().SetTrigger("PickUpToWalk");
        //_owner.grandpaModel.GetComponent<Animator>().SetTrigger("IdleToWalk");

        Debug.Log("Exit GrandpaPickup");
    }

    public override void UpdateState(Grandpa _owner)
    {
        //Debug.Log("picked up: " + isPickedUp);
        if (_owner.state == 1)
        {
            _owner.stateMachine.ChangeState(GrandpaMovement.Instance);
        }

        //if (!isPickedUp)
        //{
        //    isPickedUp = true;
        //}
    }
}


