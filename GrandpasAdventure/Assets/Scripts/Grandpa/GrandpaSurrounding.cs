using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandpaSurrounding : MonoBehaviour {

    public Grandpa grandpa;

	// Use this for initialization
	void Start () {
        //grandpa = GetComponentInParent<Grandpa>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void removeFromDetection(GameObject obj)
    {
        //Debug.Log("item removed: " + obj.name);
        grandpa.surroundings.Remove(obj);

        if (obj.GetComponent<Item>())
        {
            if (GrandpaDetection.Instance.items.Contains(obj.GetComponent<Item>()))
            {
                GrandpaDetection.Instance.items.Remove(obj.GetComponent<Item>());
            }

            
        }
        if (obj.GetComponent<PlaneChecking>())
        {

            if (obj.GetComponent<PlaneChecking>().isEnd)
            {
                GrandpaDetection.Instance.endPlaneInSight = false;
                Debug.Log("endplane ist ausserhalb des sichtfelds");
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (!grandpa.surroundings.Contains(other.gameObject))
        {
            grandpa.surroundings.Add(other.gameObject);

            //workaround, da Aufruf in Awake oder Start nicht funktioniert
            //getItemsInSight(surroundings);
        }
    }

    void OnTriggerExit(Collider other)
    {
        removeFromDetection(other.gameObject);
    }
}
