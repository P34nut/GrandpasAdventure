using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingPhaseControll : MonoBehaviour {

    public Camera cam;

   /* private void OnMouseDown()
    {
        Debug.Log("Feure Ray");       
        RaycastHit hit = new RaycastHit();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.gameObject.GetComponent<PlaneChecking>() != null)
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>().PlaceItem(hit.collider.gameObject);
            }
        }
    }*/

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Feure Ray");
            RaycastHit hit = new RaycastHit();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.GetComponent<PlaneChecking>() != null)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>().PlaceItem(hit.collider.gameObject);
                    //GetComponent<AudioSource>().Play();
                }

                if (hit.collider.gameObject.tag == "Consumable")
                {
                    Item item = hit.collider.gameObject.GetComponent<Item>();
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>().Add(item);
                    item.SendMessage("DeactivateItem");
                }
            }

        }
    }
}
