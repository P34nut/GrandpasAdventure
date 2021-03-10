using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePerformance : MonoBehaviour
{

    //Momentane Lösung muss aber noch schöner gemacht werden ohne die Objecte per public reinzuziehen 
    int currentState;

    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public GameObject room4;
    public GameObject room5;
    public GameObject room6;
    public GameObject room7;
    //public GameObject room8;

    public GameObject gang0;
    public GameObject gang1;
    public GameObject gang2;
    public GameObject gang3;
    public GameObject gang4;
    public GameObject gang5;
    public GameObject gang6;
    public GameObject gang7;

    List<GameObject> roomList;

    GameObject currentRoom;
    GameObject lastRoom;
    public GameObject grandpa;
    public GameObject grandpaPosition;
    public GameObject player;
    public GameObject topDownCam;

    public GameObject THX;

    private Gamestate gameState;

    private void Awake()
    {
        gameState = Gamestate.Instance;
    }

    // Use this for initialization
    void Start()
    {
        
        roomList = new List<GameObject>();
        roomList.Add(gang0);
        roomList.Add(room1);
        roomList.Add(gang1);
        roomList.Add(room2);
        roomList.Add(gang2);
        roomList.Add(room3);
        roomList.Add(gang3);
        roomList.Add(room4);
        roomList.Add(gang4);
        roomList.Add(room5);
        roomList.Add(gang5);
        roomList.Add(room6);
        roomList.Add(gang6);
        roomList.Add(room7);
        roomList.Add(gang7);
        //roomList.Add(room8);

        DeactivateRooms();
        SetRooms();
        TeleportPlayer.Instance.CloseDoor();
    }

    void UpdateState()
    {
        gameState.currentState++;
        SaveLoadManager.SaveGameState(gameState);
        SetRooms();
    }

    void SetRooms()
    {
        DeactivateRooms();
        Debug.Log("Setzte Räume");
        for (int i = -1; i < 3; i++)
        {
            Debug.Log("For-Schleife");
            if (gameState.currentState + i < 0)
            { }
            else
            {
                Debug.Log("Aktiviere Raum:" + gameState.currentState + i);
                roomList[gameState.currentState + i].SetActive(true);
                Debug.Log("Setzte Raum aktiv" + gameState.currentState + i);
                if (i == 0)
                    currentRoom = roomList[i+gameState.currentState];
                if (i == -1)
                    lastRoom = roomList[i + gameState.currentState];
            }
        }
    }

    void DeactivateRooms()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            roomList[i].SetActive(false);
        }
    }

    void GrandpaGoal()
    {
        THX.SetActive(true);
        Time.timeScale = 0f;


        Debug.Log("Grandpa hat Ziel erreicht");
        //UpdateState();
        //Transform child = currentRoom.transform.Find("SaveZone");
        //child.gameObject.SetActive(false);
        GetComponent<SaveZone>().zustandSenken();
        topDownCam.SetActive(false);
        grandpa.SetActive(false);
        player.SetActive(true);
        SendMessage("ActivateUI");
        Debug.Log(lastRoom.name);
        Debug.Log(currentRoom.name);
        Transform child = currentRoom.transform.Find("door");
        Debug.Log("CHILD" + child);
        child.GetComponent<Animator>().SetTrigger("CloseDoor");
        Transform childLast = lastRoom.transform.Find("door");
        //childLast.GetComponent<Animator>().SetTrigger("Close");
        //topDownCam.transform.position = currentRoom.transform.position;
        Debug.Log(currentRoom.transform.position +""+ currentRoom);
        //Animator anim = lastRoom.transform.Find("door").GetComponent<Animator>();
        //anim.SetTrigger("Close");
        //grandpaPosition.GetComponentInParent<Transform>().position = currentRoom.transform.Find("Spawn").transform.position;
        Debug.Log(currentRoom.name);
        //Transform spawn = currentRoom.transform.Find("Spawn").transform;
        //Spawn Hardcode
        //grandpaPosition.transform.position = new Vector3(-8, 0, 12);
        TeleportPlayer.Instance.TeleportOpa();

    }

    void SetTopDownCam()
    {
        //topDownCam.transform.position = currentRoom.transform.position;
        topDownCam.transform.position = new Vector3(currentRoom.transform.position.x, 12.5f, currentRoom.transform.position.z);
    }

}
