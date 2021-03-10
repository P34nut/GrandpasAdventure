using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZustandPlayer : MonoBehaviour {

    [SerializeField]
    private ZustandStat alkohol;

    [SerializeField]
    private ZustandStat gier;

    [SerializeField]
    private ZustandStat angst;

    public float getAlkohol
    {

        get
        {
            return alkohol.CurrentVal;
        }

        set
        {
            alkohol.CurrentVal = value;
        }
    }

    public float getGier
    {
        get
        {
            return gier.CurrentVal;
        }

        set
        {
            gier.CurrentVal = value;
        }
    }

    public float getAngst
    {
        get
        {
            return angst.CurrentVal;
        }

        set
        {
            angst.CurrentVal = value;
        }


    }

    private float previousAlkohol;
    private float previousGier;
    private float previousAngst;

    private Gamestate gameManager;

    private void Awake()
    {
        gameManager = Gamestate.Instance;
        alkohol.Initialize();
        gier.Initialize();
        angst.Initialize();
        SaveLoadManager.LoadZustand(this);
        //SaveLoadManager.LoadGameState(gameManager);
        if (gameManager.currentRoom == 0)
        {
            alkohol.CurrentVal = 0;
            gier.CurrentVal = 0;
            angst.CurrentVal = 0;
        }
    }

    public void IncAlkohol()
    {
        alkohol.CurrentVal++;
    }

    public void IncGier()
    {
        gier.CurrentVal++;
    }

    public void IncAngst()
    {
        angst.CurrentVal++;
    }

}
