using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    //public GameObject camera;
    private Vector3 offset;
    // Use this for initialization
	
	// Update is called once per frame
	void LateUpdate () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 6.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 6.0f;
        var y = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 150.0f;
        var yRot = Input.GetAxis("RotateCam") * Time.deltaTime * 150.0f;

        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);
        if (gameObject.transform.position.y < 8.5 && y > 0 || gameObject.transform.position.y >= 12.5 && y < 0)
        { y = 0; }

        transform.Translate(0, -y, 0);
        transform.Rotate(0, yRot, 0);
	}
}
