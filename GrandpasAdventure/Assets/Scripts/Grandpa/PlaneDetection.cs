using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDetection : MonoBehaviour {

    GrandpaDetection detection;

    // Use this for initialization
    void Awake () {
        detection = GrandpaDetection.Instance;
        Debug.Log(detection);
        Debug.Log("planedetection awake");
    }

	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlaneChecking>() != null)
        {
            PlaneChecking planeCheck = other.GetComponent<PlaneChecking>();

            Debug.Log("planedetection onTriggerEnter");

            if (planeCheck.walkable)
            {
                if (detection.freePlanes != null)
                {
                    if (!detection.freePlanes.Contains(other.gameObject))
                    {
                        detection.freePlanes.Add(other.gameObject);
                        //Debug.Log(detection.freePlanes);
                    }
                }
            }
        }
    }
    
}
