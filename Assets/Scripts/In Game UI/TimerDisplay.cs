using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TMP_Text timerText;

    public float timeLimit = 70f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timePassed = Time.time - startTime;
        float remainingTime = timeLimit - timePassed;
        int remainingTimeInSeconds = (int)remainingTime;

        if (remainingTimeInSeconds >= 0)
        {
            string minutesDisplay = getMinutes(remainingTimeInSeconds);
            string secondsDisplay = getSeconds(remainingTimeInSeconds);
            timerText.text = minutesDisplay + ":" + secondsDisplay;
        }
        else
        {
            timerText.text = "GAMEOVER";
        }
    }

    string getMinutes(int time)
    {
        int minutes = time / 60;
        if (minutes / 10 == 0)
            return "0" + minutes;
        else
            return "" + minutes;
    }

    string getSeconds(int time)
    {
        int minutes = time / 60;
        int minusSeconds = minutes * 60;
        time = time - minusSeconds;
        if (time / 10 == 0)
            return "0" + time;
        else
            return "" + time;
    }
}
