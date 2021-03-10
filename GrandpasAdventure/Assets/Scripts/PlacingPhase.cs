using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingPhase : MonoBehaviour {

    public GameObject topDownCam;
    public GameObject uiGrandpaButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            /*Debug.Log("Starte platzierphase");
            other.gameObject.SetActive(false);
            Debug.Log("Player deaktiviert");
            topDownCam.SetActive(true);
            uiGrandpaButton.SetActive(true);
            Debug.Log("Platzierphase aktiviert");
            Cursor.lockState = CursorLockMode.None;
            GameObject[] floorArray = GameObject.FindGameObjectsWithTag("Floor");

            for (int i = 0; i < floorArray.Length; i++)
            {
                if (floorArray[i].GetComponent<PlaneChecking>())
                {
                    floorArray[i].SendMessage("CheckLight");
                }
            }*/
            GameObject.FindGameObjectWithTag("GameController").SendMessage("StartPlacingPhase");
            Destroy(gameObject);
        }

    }

    private void StartPlacingPhase()
    {

    }

}
