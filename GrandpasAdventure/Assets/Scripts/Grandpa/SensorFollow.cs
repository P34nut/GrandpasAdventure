using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorFollow : MonoBehaviour {

    public GameObject parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = parent.transform.position;
	}
}
