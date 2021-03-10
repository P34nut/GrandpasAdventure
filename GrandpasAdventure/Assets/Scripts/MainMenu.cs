using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private Gamestate gameManager;
    public GameObject gameStateManagerPrefab;

    private void Awake()
    {
        if (GameObject.Find("GameStateManager") ==null)
        {
            GameObject gsm = Instantiate<GameObject>(gameStateManagerPrefab);
            gsm.name = "GameStateManager";
        }
    }

    // Use this for initialization
    void Start () {
        gameManager = Gamestate.Instance;

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void NewGame()
    {
        LoadingScreenManager.LoadScene(1);
        gameManager.currentRoom = 0;
        gameManager.currentState = 0;
        SaveLoadManager.SaveGameState(gameManager);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        switch (gameManager.currentRoom)
        {
            case 0:
                gameManager.currentState = 0;
                break;
            case 1:
                gameManager.currentState = 2;
                break;
            case 2:
                gameManager.currentState = 4;
                break;
            case 3:
                gameManager.currentState = 6;
                break;
            case 4:
                gameManager.currentState = 8;
                break;
            case 5:
                gameManager.currentState = 10;
                break;
            case 6:
                gameManager.currentState = 12;
                break;
            case 7:
                gameManager.currentState = 14;
                break;
        }
        SaveLoadManager.SaveGameState(gameManager);
        LoadingScreenManager.LoadScene(1);
    }

}
