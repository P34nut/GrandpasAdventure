using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour {

    public Grandpa grandpa;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IdleStart()
    {
        Debug.Log("Idle animation start");
    }

    void PickUpStart()
    {
        Debug.Log("PickUp animation start");
    }
    
    void PickUpEnd()
    {
        Debug.Log("PickUp animation end");
        grandpa.state--;
    }

    void StartWalk()
    {
        Debug.Log("Walk animatoin start");
    }

    void EndWalk()
    {
        Debug.Log("Walk animatoin end");
    }
}
