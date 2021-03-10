using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FackelPickup : MonoBehaviour
{

    public GameObject torch;
    public GameObject handTorch;

    public void SwapFlares()
    {

            handTorch.SetActive(true);
            Debug.Log("Aktiviere Handfackel");

    
    }
}
