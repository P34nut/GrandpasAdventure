using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    Inventory inventory;

    InventroySlot[] slots;
    public  Text AlcoholCounterUI;
    public Text GreedCounterUI;
    public Text FearCounterUI;
    // Use this for initialization
    void Start () {
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        //inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventroySlot>();

        AlcoholCounterUI.text = "0";
        GreedCounterUI.text = "0";
        FearCounterUI.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateUI(List<GameObject> Alcohol, List<GameObject> Greed, List<GameObject> Fear)
    {
        Debug.Log("Updating UI");
        AlcoholCounterUI.text = Alcohol.Count.ToString();
        GreedCounterUI.text = Greed.Count.ToString();
        FearCounterUI.text = Fear.Count.ToString();
    }
}
