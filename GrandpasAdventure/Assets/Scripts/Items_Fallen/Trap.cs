using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public GameObject trapActivatet;
    public GameObject trapDeactivatet;
    bool isActiv = true;
    public GameObject objectToDefuse;
    public AudioClip[] clips;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {

        audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Grandpa" && isActiv)
        {
            if( audioSource!= null)
            {
                audioSource.clip = clips[0];
                audioSource.Play();
            }
            else
            {
                Debug.Log("No Audio");
            }
           
            other.SendMessage("PlayerIsDead");
            TriggerTrap();
        }
    }

    void TriggerTrap()
    {
        //gameObject.SetActive(false);
        RecDisabler(transform);
        Instantiate(trapActivatet, gameObject.transform);
    }

    void DeactivateTrap(GameObject handObject)
    {
        Debug.Log("Probiere Falle zu deaktivieren");
        if (objectToDefuse.tag == "Player")
        {
            Debug.Log("Entschärfen funktioniert");
            gameObject.GetComponentInParent<PlaneChecking>().isDeactivated = true;
            RecDisabler(transform);
            audioSource.clip = clips[2];
            audioSource.Play();
            Instantiate(trapDeactivatet, gameObject.transform);
            Collider collider = gameObject.GetComponent<BoxCollider>();
            collider.enabled = !collider.enabled;
            if (handObject.tag == "CamRay")
                handObject.SendMessage("DeleteHanditem");
        }
        else
        if (handObject != null && handObject.tag != "CamRay")
        {
            Debug.Log(handObject.name);
            if (objectToDefuse.name == handObject.GetComponent<Item>().name)
            {
                Debug.Log("Entschärfen funktioniert");
                gameObject.GetComponentInParent<PlaneChecking>().isDeactivated = true;
                RecDisabler(transform);
                audioSource.clip = clips[1];
                audioSource.Play();
                Instantiate(trapDeactivatet, gameObject.transform);
                Collider collider = gameObject.GetComponent<BoxCollider>();
                collider.enabled = !collider.enabled;
                GameObject.FindGameObjectWithTag("CamRay").SendMessage("DeactivateHanditem");
            }
            else
            {
                Debug.Log("Platzieren nicht möglich");
                return;
            }
        }
        else if (handObject.tag == "CamRay")
            GameObject.FindGameObjectWithTag("CamRay").SendMessage("DeleteHanditem");
        GameObject.FindGameObjectWithTag("CamRay").SendMessage("DeleteTrap");
        SendMessageUpwards("IsBlocked");
    }
    
    void ActivateTrap()
    {
        gameObject.GetComponentInParent<PlaneChecking>().isDeactivated = false;
        Collider collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = !collider.enabled;
        RecDisabler(transform);
        
    }

    void RecDisabler(Transform t)
    {
        if (t.childCount > 0)
        {
            foreach (Transform child in t)
            {
                RecDisabler(child);
            }
        }
        if (t.gameObject.GetComponent<MeshRenderer>() != null)
            t.gameObject.GetComponent<MeshRenderer>().enabled = !t.gameObject.GetComponent<MeshRenderer>().enabled;
    }

    //für erneute Platzierphase
    public void Deactivate()
    {
        RecDisabler(transform);
        Instantiate(trapDeactivatet, gameObject.transform);
        Collider collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = !collider.enabled;
    }
}
