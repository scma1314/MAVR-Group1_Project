using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using TMPro;

public class Timer : MonoBehaviour

{

    public float timeValue = 0;

    public TMP_Text timerText;

    public bool Timer_running = false;

    //Update is called once per frame

    void Update()

    {

        if (Timer_running)
        {
            timeValue += Time.deltaTime;
            DisplayTime(timeValue);
        }

            

    }

    void DisplayTime(float timeToDisplay)

    {

        if (timeToDisplay < 0)

        {

            timeToDisplay = 0;

        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        float milliseconds = timeToDisplay % 1 * 1000;

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

    }

}