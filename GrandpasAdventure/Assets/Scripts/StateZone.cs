using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerRadius")
        {
            GameObject.FindGameObjectWithTag("GameController").SendMessage("UpdateState");
            Destroy(gameObject);
        }
    }
}
