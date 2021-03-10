using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestate : MonoBehaviour {

    public static Gamestate Instance;


    public int currentRoom;
    public int currentState;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SaveLoadManager.LoadGameState(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
