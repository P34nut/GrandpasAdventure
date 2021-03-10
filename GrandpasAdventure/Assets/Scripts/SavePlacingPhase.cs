using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlacingPhase : MonoBehaviour {

    public static SavePlacingPhase Instance;
    public List<GameObject> floorList = new List<GameObject>();
    public GameObject[] floorObjects;
    public bool[] isLighting;
    public bool[] trapIsDeactivated;
    public bool[] isBlocked;
    public bool[] isHanditem;
    public bool isReloaded;
    public GameObject player;
    public GameObject handfackel;
    public GameObject wandfackel;
    public Vector3[] positions;
    public GameObject[] saveZones;
    public Animator[] doors;
    private string roomName;

    private void Awake()
    {
        Instance = this;
        SaveLoadManager.LoadPlacingData(this);
    }

    // Use this for initialization
    void Start () {
        FindFloorInRoom();
        if (isReloaded)
        {
            LoadPlacingPhase();
            isReloaded = false;
            SaveLoadManager.SavePlacingData(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FindFloorInRoom()
    {
        floorList.Clear();

        switch (Gamestate.Instance.currentRoom)
        {
            case 0:
                roomName = "Raum1";
                break;
            case 1:
                roomName = "Raum2";
                break;
            case 2:
                roomName = "Raum3";
                break;
            case 3:
                roomName = "Raum4";
                break;
            case 4:
                roomName = "Raum5";
                break;
            case 5:
                roomName = "Raum6";
                break;
            case 6:
                roomName = "Raum7";
                break;
        }

        Transform parent = GameObject.Find(roomName).transform;
        GetChildObject(parent);
        floorObjects = floorList.ToArray();
        Debug.Log("Length "+ floorObjects.Length);
    }

    void GetChildObject(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == "Floor")
            {
                if (child.GetComponent<PlaneChecking>())
                {
                    floorList.Add(child.gameObject);
                }
                
            }
            if (child.childCount > 0)
            {
                GetChildObject(child);
            }
        }
    }


    public void SaveAll()
    {

        isLighting = new bool[floorObjects.Length];
        trapIsDeactivated = new bool[floorObjects.Length];
        isBlocked = new bool[floorObjects.Length];
        isHanditem = new bool[floorObjects.Length];
        for (int i = 0; i < floorObjects.Length; i++)
        {
            if (floorObjects[i].GetComponent<PlaneChecking>())
            {
                isLighting[i] = floorObjects[i].GetComponent<PlaneChecking>().playerTouched;
                trapIsDeactivated[i] = floorObjects[i].GetComponent<PlaneChecking>().isDeactivated;
                isBlocked[i] = floorObjects[i].GetComponent<PlaneChecking>().blocked;
                if (floorObjects[i].GetComponent<PlaneChecking>().blocked && floorObjects[i].transform.childCount>1)
                {
                    if (floorObjects[i].transform.GetChild(1).tag == "Handitem")
                    {
                        isHanditem[i] = true;
                    }
                    else
                    {
                        isHanditem[i] = false;
                    }

                }
                else
                {
                    isHanditem[i] = false;
                }
            }
        }
        SaveLoadManager.SavePlacingData(this);
        Inventory.Instance.GetListCount();
    }

    public void LoadPlacingPhase()
    {
        
        for (int i = 0; i < floorObjects.Length; i++)
        {
            floorObjects[i].GetComponent<PlaneChecking>().playerTouched = isLighting[i];
            //floorObjects[i].SendMessage("CheckLight");
            floorObjects[i].GetComponent<PlaneChecking>().isDeactivated = trapIsDeactivated[i];
            floorObjects[i].GetComponent<PlaneChecking>().blocked = isBlocked[i];

            if (trapIsDeactivated[i])
            {
                floorObjects[i].transform.GetChild(1).GetComponent<Trap>().Deactivate();
            }

            if (!isBlocked[i] && floorObjects[i].transform.childCount > 1)
            {
                Destroy(floorObjects[i].transform.GetChild(1).gameObject);
            }

            if (isHanditem[i])
            {
                Instantiate(Resources.Load("Baumstamm"), floorObjects[i].transform);
            }
            

        }
        
        //anim.SetBool("OpenTheDoor", true);
        Destroy(wandfackel);
        handfackel.SetActive(true);
       
        Inventory.Instance.AddFromPrefabAfterReload();
        PauseMenu.Instance.StartPlacingPhase();
        Teleport();
    }

    public void ReloadPlacingPhase()
    {
        Gamestate.Instance.currentState = 1;
        SaveLoadManager.SaveGameState(Gamestate.Instance);
        isReloaded = true;
        SaveLoadManager.SavePlacingData(this);
        //Inventory.Instance.GetListCount();
        LoadingScreenManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    void Teleport()
    {
        int i = Gamestate.Instance.currentRoom;
        
        switch (i)
        {
            case 0:
                player.transform.position = positions[i];
                Destroy(saveZones[i]);
                doors[i].SetTrigger("OpenDoor");
                break;
            case 1:
                player.transform.position = positions[i];
                Destroy(saveZones[i]);
                doors[i].SetTrigger("OpenDoor");
                break;
            case 2:
                player.transform.position = positions[i];
                player.transform.Rotate(0, 90, 0, Space.World);
                Destroy(saveZones[i]);
                doors[i].SetTrigger("OpenDoor");
                break;
            case 3:
                player.transform.position = positions[i];
                player.transform.Rotate(0, 180, 0, Space.World);
                Destroy(saveZones[i]);
                doors[i].SetTrigger("OpenDoor");
                break;
            case 4:
                player.transform.position = positions[i];
                player.transform.Rotate(0, 180, 0, Space.World);
                Destroy(saveZones[i]);
                doors[i].SetTrigger("OpenDoor");
                break;
            case 5:
                player.transform.position = positions[i];
                player.transform.Rotate(0, 270, 0, Space.World);
                Destroy(saveZones[i]);
                doors[i].SetTrigger("OpenDoor");
                break;
            case 6:
                player.transform.position = positions[i];
                Destroy(saveZones[i]);
                doors[i].SetTrigger("OpenDoor");
                break;
        }
    }

}
