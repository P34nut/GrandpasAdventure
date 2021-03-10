using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static PauseMenu Instance;

    public GameObject PauseMenuPanel;
    private bool isOpen;
    public GameObject grandpaPhaseButton;
    public GameObject grandpa;
    public GameObject topDownCam;
    public GameObject player;
    //public GameObject uiGrandpaButton;
    public GameObject inventoryUI;
    public GameObject timerUI;
    bool placingPhase = false;
    private Gamestate gameState;

    private void Awake()
    {
        Instance = this;
        PauseMenuPanel.SetActive(false);
        gameState = Gamestate.Instance;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) && !isOpen)
        {
            PauseMenuPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
            Time.timeScale = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            Time.timeScale = 1.0f;
            PauseMenuPanel.SetActive(false);
            if (!placingPhase)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
           

        }

	}




    public void QuitGame()
    {
        Application.Quit();
        Time.timeScale = 1.0f;
    }

    public void RestartRoom()
    {
        switch (gameState.currentRoom)
        {
            case 0:
                gameState.currentState = 0;
                break;
            case 1:
                gameState.currentState = 2;
                break;
            case 2:
                gameState.currentState = 4;
                break;
            case 3:
                gameState.currentState = 6;
                break;
            case 4:
                gameState.currentState = 8;
                break;
            case 5:
                gameState.currentState = 10;
                break;
            case 6:
                gameState.currentState = 12;
                break;
            case 7:
                gameState.currentState = 14;
                break;
        }
        SaveLoadManager.SaveGameState(gameState);
        LoadingScreenManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void ContinueGame()
    {
        PauseMenuPanel.SetActive(false);
        isOpen = false;
        Time.timeScale = 1.0f;
        if (!placingPhase)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
       
    }

    public void BackToMain()
    {
        LoadingScreenManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void StartGrandpaPhase()
    {
        grandpa.SetActive(true);
        topDownCam.GetComponent<PlacingPhaseControll>().enabled = false;
        topDownCam.transform.position = new Vector3(grandpa.transform.position.x - 5f, grandpa.transform.position.y + 10f, grandpa.transform.position.z);
        grandpaPhaseButton.SetActive(false);
        inventoryUI.SetActive(false);
        timerUI.SetActive(false);
        GameObject[] floorArray = GameObject.FindGameObjectsWithTag("Floor");

        for (int i = 0; i < floorArray.Length; i++)
        {
            if (floorArray[i].GetComponent<PlaneChecking>())
            {
                floorArray[i].SendMessage("CheckLight");
            }
        }
    }

    public void StartPlacingPhase()
    {

        player.SetActive(false);
        topDownCam.SetActive(true);
        grandpaPhaseButton.SetActive(true);
        //uiGrandpaButton.SetActive(true);
        SendMessage("SetTopDownCam");
        SendMessage("UpdateState");
        //topDownCam.transform.position = currentRoom.transform.position;
        //SendMessage("SetTopDownCam");
        Cursor.lockState = CursorLockMode.None;
        SavePlacingPhase.Instance.SaveAll();
        GameObject[] floorArray = SavePlacingPhase.Instance.floorObjects;

        for (int i = 0; i < floorArray.Length; i++)
        { 
            floorArray[i].SendMessage("CheckLight");
        }
        placingPhase = true;

      
    }

    public void FPStoGrandpa()
    {
        if (!placingPhase)
        {
            StartPlacingPhase();
        }
        StartGrandpaPhase();
    }

    void ActivateUI()
    {
        inventoryUI.SetActive(true);
        timerUI.SetActive(true);
        timerUI.SendMessage("ResetTime");

    }

    public void SetTimeScale()
    {
        Time.timeScale = 1f;
    }

}
