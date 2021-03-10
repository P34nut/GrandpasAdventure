using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : MonoBehaviour {

    public TestZustandPlayer opaSkript;
    public Gamestate gameManager;
    public int room;

    private bool checkedPrevious;

    private float previousAlkohol;
    private float previousGier;
    private float previousAngst;


    private void Awake()
    {
        gameManager = Gamestate.Instance;
    }
    // Use this for initialization
    void Start () {

        

    }
	
	// Update is called once per frame
	void Update () {

        if (room == gameManager.currentRoom && !checkedPrevious)
        {
            checkedPrevious = true;
            previousAlkohol = opaSkript.getAlkohol;
            previousAngst = opaSkript.getAngst;
            previousGier = opaSkript.getGier;
        }
    }

    public void zustandSenken()
    {

        Debug.Log("richtiger Collider");

        //Destroy(other.gameObject);

        if (previousAlkohol == opaSkript.getAlkohol)
        {
            opaSkript.getAlkohol -= 1;
        }
        if (previousAngst == opaSkript.getAngst)
        {
            opaSkript.getAngst -= 1;
        }
        if (previousGier == opaSkript.getGier)
        {
            opaSkript.getGier -= 1;
        }

        gameManager.currentRoom += 1;

        SaveLoadManager.SaveZustand(opaSkript);
        SaveLoadManager.SaveGameState(gameManager);
    
    }

}
