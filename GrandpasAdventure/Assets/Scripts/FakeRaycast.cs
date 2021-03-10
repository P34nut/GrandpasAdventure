using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeRaycast : MonoBehaviour
{

    private GameObject activePlane;
    private GameObject activeTrap;

    private void OnTriggerStay(Collider other)
    {        
        if (other.tag == "Floor")
        {
            activePlane = other.gameObject;
            SendMessageUpwards("DeleteTrap");
            SendMessageUpwards("SetPlane", activePlane);
        }
        else if (other.tag == "Trap")
        {
            //Debug.Log("Sehe Falle");
            activeTrap = other.gameObject;
            SendMessageUpwards("DeletePlane");
            SendMessageUpwards("SetTrap", activeTrap);
        }
        else
        {
            activePlane = null;
            activeTrap = null;
            SendMessageUpwards("DeletePlane");
            SendMessageUpwards("DeleteTrap");
        }

      
    }

    private void OnTriggerExit(Collider other)
    {
        activePlane = null;
        activeTrap = null;
        SendMessageUpwards("DeletePlane");
        SendMessageUpwards("DeleteTrap");
    }
}
