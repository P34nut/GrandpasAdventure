using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour {

    public static TeleportPlayer Instance;

    private int currentRoom;
    private Gamestate gameManager;

    public Vector3[] positions;
    public Vector3[] positionsOpa;
    public GameObject[] saveZones;
    public Animator[] doors;
    public GameObject handfackel;
    public GameObject opa;

    private void Awake()
    {
        Instance = this;
        gameManager = Gamestate.Instance;
        SaveLoadManager.LoadGameState(gameManager);
    }

    // Use this for initialization
    void Start () {
        currentRoom = gameManager.currentRoom;
        teleport();
        TeleportOpa();
        //CloseDoor();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void teleport()
    {
        int maxRooms = 7;
        for (int i = 0; i < maxRooms; i++)
        {
            if (i == currentRoom)
            {
                transform.position = positions[i];

                switch (i)
                {
                    case 3:
                        transform.Rotate(0, 90, 0, Space.World);
                        break;
                    case 4:
                        transform.Rotate(0, 180, 0, Space.World);
                        break;
                    case 5:
                        transform.Rotate(0, 180, 0, Space.World);
                        break;
                    case 6:
                        transform.Rotate(0, 270, 0, Space.World);
                        break;
                }

                if (i > 0)
                {
                    handfackel.SetActive(true);
                    Destroy(saveZones[i - 1]);
                }
            }
        }
    }

    public void CloseDoor()
    {
        switch (currentRoom)
        {
            case 1:
                Debug.Log("CLOSING");
                doors[0].SetTrigger("CloseDoor");
                Destroy(doors[0].gameObject.GetComponent<DoorAnimation>());
                break;
            case 2:
                doors[1].SetTrigger("CloseDoor");
                Destroy(doors[1].gameObject.GetComponent<DoorAnimation>());
                break;
            case 3:
                doors[2].SetTrigger("CloseDoor");
                Destroy(doors[2].gameObject.GetComponent<DoorAnimation>());
                break;
            case 4:
                doors[3].SetTrigger("CloseDoor");
                Destroy(doors[3].gameObject.GetComponent<DoorAnimation>());
                break;
            case 5:
                doors[4].SetTrigger("CloseDoor");
                Destroy(doors[4].gameObject.GetComponent<DoorAnimation>());
                break;
            case 6:
                doors[5].SetTrigger("CloseDoor");
                Destroy(doors[5].gameObject.GetComponent<DoorAnimation>());
                break;
            case 7:
                doors[6].SetTrigger("CloseDoor");
                Destroy(doors[6].gameObject.GetComponent<DoorAnimation>());
                break;
        }
    }

    public void TeleportOpa()
    {
        switch (currentRoom)
        {
            case 0:
                opa.transform.position = positionsOpa[currentRoom];
                break;
            case 1:
                opa.transform.position = positionsOpa[currentRoom];
                break;
            case 2:
                opa.transform.position = positionsOpa[currentRoom];
                break;
            case 3:
                opa.transform.position = positionsOpa[currentRoom];
                opa.transform.Rotate(0, 90, 0, Space.World);
                break;
            case 4:
                opa.transform.position = positionsOpa[currentRoom];
                opa.transform.Rotate(0, 180, 0, Space.World);
                break;
            case 5:
                opa.transform.position = positionsOpa[currentRoom];
                opa.transform.Rotate(0, 180, 0, Space.World);
                break;
            case 6:
                opa.transform.position = positionsOpa[currentRoom];
                opa.transform.Rotate(0, 270, 0, Space.World);
                break;
            case 7:
                opa.transform.position = positionsOpa[currentRoom];
                break;
        }
    }


}
