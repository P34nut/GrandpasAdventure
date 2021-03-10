using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    // Callback which is triggered when an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public InventoryUI InventoryUI;
    public Camera TopDownCam;
    private GameObject activeItem;

    public int alcoholCounter;
    public int greedCounter;
    public int fearCounter;
    public GameObject alcoholPrefab;
    public GameObject greedPrefab;
    public GameObject fearPrefab;

    private List<GameObject> AlcoholList = new List<GameObject>();
    private List<GameObject> GreedList = new List<GameObject>();
    private List<GameObject> FearList = new List<GameObject>();


    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Debug.Log("Add item to inventory");
        string itemFunktion = item.name;

        switch (itemFunktion)
        {
            case "Alcohol":
                AlcoholList.Add(item.gameObject);;
                break;
            case "Greed":
                GreedList.Add(item.gameObject);
                break;
            case "Fear":
                FearList.Add(item.gameObject);
                break;
            default:
                break;
        }
        Debug.Log("Update UI");
        InventoryUI.UpdateUI(AlcoholList, GreedList, FearList);
    }

    // Remove an item
    public void RemoveItem(Item item)
    {
        string itemFunktion = item.name;

        switch (itemFunktion)
        {
            case "Alcohol":
                AlcoholList.Remove(item.gameObject);
                break;
            case "Greed":
                GreedList.Remove(item.gameObject);
                break;
            case "Fear":
                FearList.Remove(item.gameObject);
                break;
            default:
                break;
        }
        activeItem = null;
        InventoryUI.UpdateUI(AlcoholList, GreedList, FearList);
    }

    public void ActivateAlcohol()
    {
        if (AlcoholList.Count > 0)
        {           
            activeItem = AlcoholList[0];
            Debug.Log(activeItem);
        }
    }

    public void ActivateGreed()
    {
        if (GreedList.Count > 0)
        {
            activeItem = GreedList[0];
            Debug.Log(activeItem);
        }
    }

    public void ActivateFear()
    {
        if (FearList.Count > 0)
        {
            activeItem = FearList[0];
            Debug.Log(activeItem);
        }
    }

    public void PlaceItem(GameObject Plane)
    {
        Debug.Log("trying to place Item");
        if (activeItem == null)
            return;

        PlaneChecking planeChecking = Plane.GetComponent<PlaneChecking>();

        if (!planeChecking.blocked && planeChecking.playerTouched)
        {
            Instantiate(Resources.Load(activeItem.GetComponent<Item>().modell), Plane.transform);
            Camera.main.GetComponentInParent<AudioSource>().Play();
            planeChecking.blocked = true;
            RemoveItem(activeItem.GetComponent<Item>());

        }
    }

    public void GetListCount()
    {
        alcoholCounter = AlcoholList.Count;
        greedCounter = GreedList.Count;
        fearCounter = FearList.Count;
        SaveLoadManager.SaveInventoryData(this);
    }

    public void AddFromPrefabAfterReload()
    {

        SaveLoadManager.LoadInventoryData(this);

        AlcoholList.Clear();
        GreedList.Clear();
        FearList.Clear();

        for (int i = 0; i < alcoholCounter; i++)
        {
            AlcoholList.Add(alcoholPrefab);
        }
        for (int j = 0; j < greedCounter; j++)
        {
            GreedList.Add(greedPrefab);
        }
        for (int k = 0; k < fearCounter; k++)
        {
            FearList.Add(fearPrefab);
        }

        InventoryUI.UpdateUI(AlcoholList, GreedList, FearList);

    }


}