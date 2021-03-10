using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public static class SaveLoadManager {

    public static void SaveGameState(Gamestate gameManager)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/gameState.gs", FileMode.Create);

        GameStateData data = new GameStateData(gameManager);

        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("GS-GESPEICHERT");
    }

    public static void SaveZustand(TestZustandPlayer opa)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/zustand.zs", FileMode.Create);

        ZustandData data = new ZustandData(opa);

        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("ZS-GESPEICHERT");
    }

    public static void SavePlacingData(SavePlacingPhase placingPhase)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/placingData.pd", FileMode.Create);

        PlacingData data = new PlacingData(placingPhase);

        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("PD-GESPEICHERT");
    }

    public static void SaveInventoryData(Inventory inventory)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/inventoryData.id", FileMode.Create);

        InventoryData data = new InventoryData(inventory);

        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("ID-GESPEICHERT");
    }

    public static void LoadZustand(TestZustandPlayer opa)
    {
        if (File.Exists(Application.persistentDataPath + "/zustand.zs"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/zustand.zs", FileMode.Open);

            ZustandData data = bf.Deserialize(stream) as ZustandData;

            stream.Close();
            opa.getAlkohol = data.alkohol;
            opa.getAngst = data.angst;
            opa.getGier = data.gier;
        }
        else
        {
            Debug.LogError("ZS-File does not exist");
            opa.getAlkohol = 0;
            opa.getAngst = 0;
            opa.getGier = 0;
            
        }
    }

    public static void LoadGameState(Gamestate gameManager)
    {
        if (File.Exists(Application.persistentDataPath + "/gameState.gs"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/gameState.gs", FileMode.Open);

            GameStateData data = bf.Deserialize(stream) as GameStateData;

            stream.Close();
            gameManager.currentRoom = data.roomIndex;
            gameManager.currentState = data.currentState;
        }
        else
        {
            Debug.LogError("GS-File does not exist");
            gameManager.currentRoom = 0;
            gameManager.currentState = 0;
        }
    }

    public static void LoadPlacingData(SavePlacingPhase placingPhase)
    {
        if (File.Exists(Application.persistentDataPath + "/placingData.pd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/placingData.pd", FileMode.Open);

            PlacingData data = bf.Deserialize(stream) as PlacingData;

            stream.Close();
            placingPhase.isLighting = data.isLighting;
            placingPhase.trapIsDeactivated = data.isDeactivated;
            placingPhase.isBlocked = data.isBlocked;
            placingPhase.isHanditem = data.isHandItem;
            placingPhase.isReloaded = data.isReloaded;
        }
        else
        {
            Debug.LogError("PD-File does not exist");
            placingPhase.isLighting = new bool[0];
            placingPhase.trapIsDeactivated = new bool[0];
            placingPhase.isHanditem = new bool[0];
            placingPhase.isBlocked = new bool[0];
            placingPhase.isReloaded = false;
        }
    }

    public static void LoadInventoryData(Inventory inventory)
    {
        if (File.Exists(Application.persistentDataPath + "/inventoryData.id"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/inventoryData.id", FileMode.Open);

            InventoryData data = bf.Deserialize(stream) as InventoryData;

            stream.Close();
            inventory.alcoholCounter = data.alcohol;
            inventory.greedCounter = data.greed;
            inventory.fearCounter = data.fear;
        }
        else
        {
            Debug.LogError("ID-File does not exist");
            inventory.alcoholCounter = 0;
            inventory.greedCounter = 0;
            inventory.fearCounter = 0;
        }
    }
    

}

[Serializable]
public class GameStateData
{
    public int roomIndex;
    public int currentState;

    public GameStateData(Gamestate gameManager)
    {
        roomIndex = gameManager.currentRoom;
        currentState = gameManager.currentState;
    }
}

[Serializable]
public class ZustandData
{
    public float alkohol;
    public float gier;
    public float angst;

    public ZustandData(TestZustandPlayer opa)
    {
        alkohol = opa.getAlkohol;
        gier = opa.getGier;
        angst = opa.getAngst;
    }
}

[Serializable]
public class PlacingData
{
    public bool[] isLighting;
    public bool[] isDeactivated;
    public bool[] isBlocked;
    public bool[] isHandItem;
    public bool isReloaded;

    public PlacingData(SavePlacingPhase placingPhase)
    {
        isLighting = placingPhase.isLighting;
        isDeactivated = placingPhase.trapIsDeactivated;
        isHandItem = placingPhase.isHanditem;
        isBlocked = placingPhase.isBlocked;
        isReloaded = placingPhase.isReloaded;
    }
}

[Serializable]
public class InventoryData
{
    public int alcohol;
    public int greed;
    public int fear;

    public InventoryData(Inventory inventory)
    {
        alcohol = inventory.alcoholCounter;
        greed = inventory.greedCounter;
        fear = inventory.fearCounter;
    }

}
