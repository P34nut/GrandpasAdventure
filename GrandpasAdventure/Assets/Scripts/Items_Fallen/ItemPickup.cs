using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private GameObject gameController;
    private GameObject handObject;

    public GameObject wandfackel_leer;
    public GameObject torchHand;
    public GameObject mirror;
    public GameObject log;

    public GameObject cam;
    private GameObject activePlane;
    private GameObject activeTrap;


    private void OnTriggerStay(Collider other)
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory inventory = gameController.GetComponent<Inventory>();
            switch (other.tag)
            {
                case "Consumable":
                    Item item = other.GetComponent<Item>();
                    inventory.Add(item);
                    item.SendMessage("DeactivateItem");
                    break;

                case "Handitem":
                    AddHanditem(other.gameObject);
                    break;

                case "Trap":
                    if (handObject == null)
                        handObject = gameObject;
                    activeTrap.SendMessage("DeactivateTrap", handObject);
                    //handObject = null;
                    break;

               /* case "Floor":
                    if (handObject != null && handitemPickup == false)
                        DropHanditem();
                    break;*/

                default:
                    break;
            }
            if (other.GetComponent<Item>() != null)
            {
                if (other.GetComponent<Item>().name == "Fackel")
                {
                    other.GetComponent<AudioSource>().Stop();
                    other.GetComponent<Item>().SendMessage("DeactivateItem");
                    SwapTorches();
                }
            }
        }
    }

    private void SwapTorches()
    {

        torchHand.SetActive(true);
        wandfackel_leer.SetActive(true);

    }

    private void AddHanditem(GameObject handPickup)
    {       
        if (handObject != null && handObject.tag != "Player")
        {
            return;
        }
        Item item = handPickup.GetComponent<Item>();
        string itemName = item.name;

        switch (itemName)
        {
            case "Spiegel":
                mirror.SetActive(true);
                handObject = mirror;
                break;
            case "Baumstamm":
                log.SetActive(true);
                handObject = log;
                break;
            default:
                break;
        }

        item.SendMessage("DeactivateItem");


    }

    private void DropHanditem()
    {
        Debug.Log(handObject);
        if (handObject == null || activePlane == null && activeTrap == null)
        {
            return;
        }

        if (activePlane != null)
        {
            if (activePlane.GetComponent<PlaneChecking>().blocked)
            {
                return;
            }
        }

        if (activeTrap != null)
        {
            activeTrap.SendMessage("DeactivateTrap", handObject);
            return;
        }

        string handObjectname = handObject.GetComponent<Item>().name;

        switch (handObjectname)
        {
            case "Spiegel":
                mirror.SetActive(false);
                PlaceObject(handObjectname, activePlane);
                activePlane.SendMessage("IsBlocked");
                break;
            case "Baumstamm":
                log.SetActive(false);
                PlaceObject(handObjectname, activePlane);
                activePlane.SendMessage("IsBlocked");
                break;
            default:
                break;
        }
    }

    private void PlaceObject(string objectName, GameObject plane)
    {
        Instantiate(Resources.Load(objectName), plane.transform);
        handObject = null;
    }

    private void SetPlane(GameObject plane)
    {
        activePlane = plane;
    }

    private void DeletePlane()
    {
        activePlane = null;
    }

    private void SetTrap(GameObject trap)
    {
        activeTrap = trap;
    }

    private void DeleteTrap()
    {
        activeTrap = null;
    }

    private void DeactivateHanditem()
    {
        string handObjectname = handObject.GetComponent<Item>().name;


        switch (handObjectname)
        {
            case "Spiegel":
                mirror.SetActive(false);
                break;
            case "Baumstamm":
                log.SetActive(false);
                break;
            default:
                break;
        }

        handObject = null;
    }

    public void DeleteHanditem()
    {
        handObject = null;
    }
}

