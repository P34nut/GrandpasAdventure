using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

    Image timerImage;
    public float timeAmount;
    float time;
    bool timeOver = false;


	// Use this for initialization
	void Start () {

        timerImage = this.GetComponent<Image>();
        time = timeAmount;

	}
	
	// Update is called once per frame
	void Update () {

        if (time > 0)
        {
            time -= Time.deltaTime;
            timerImage.fillAmount = time / timeAmount;
        }

        if (time <= 0 && !timeOver)
        {
            timeOver = true;
            GameObject.FindGameObjectWithTag("GameController").SendMessage("FPStoGrandpa");
        }

	}

    void ResetTime()
    {
        time = timeAmount;
    }
}
