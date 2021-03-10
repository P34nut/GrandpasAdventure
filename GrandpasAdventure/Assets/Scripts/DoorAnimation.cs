using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour {

    Animator anim;
    bool playedSound;
    private void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Use this for initialization
    /* private void OnTriggerEnter(Collider other)
     {
         Debug.Log("Collidiere");
         if (other.gameObject.tag == "CamRay")
         {
             Debug.Log("Berühre CamRay");
             if (Input.GetKeyDown(KeyCode.E))
             {
                 anim.SetTrigger("Open");
                 Debug.Log("setzte Trigger");
             }
         }
     }
     */

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Collidiere");
        if (other.gameObject.tag == "CamRay" || other.gameObject.tag == "Player")
        {
            //Debug.Log("Berühre CamRay");
            if (Input.GetKeyDown(KeyCode.E))
            {
                //anim.SetBool("OpenTheDoor", true);
                if (!playedSound)
                {
                    anim.SetTrigger("OpenDoor");
                    GetComponent<AudioSource>().Play();
                    playedSound = true;
                }
                
                //Debug.Log("setzte Trigger");
            }
        }
    }

}
