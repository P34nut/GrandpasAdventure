using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour {

    //public PlayerController playerControllerScript;
    //public ItemPickup itemPickupScript;
    public GameObject gameOver;
    public GameObject restartPlacingButton;
    public AudioClip deathSound;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayerIsDead() {
        //playerControllerScript.enabled = false;
        //itemPickupScript.enabled = false;
        GetComponentInChildren<ItemPickup>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        StartCoroutine(waitForDeath());
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().clip = deathSound;
        GetComponent<AudioSource>().Play();
        Debug.Log("Player ist gestorben");
    }

    private IEnumerator waitForDeath()
    {
        yield return new WaitForSeconds(1f);
        restartPlacingButton.SetActive(false);
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        StopCoroutine(waitForDeath());
    }


}
