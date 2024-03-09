using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshPro tmp;
    public int time = 0;
    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        StartTimer();
    }

    // Update is called once per frame
    void StartTimer()
    {
        time = 0;
        InvokeRepeating("UpdateTimer", 1, 1);
    }

    void UpdateTimer()
    {
        time++;
        int seconds = time % 60;
        int minutes = Mathf.FloorToInt(time / 60f);

        string text = "<mspace=.5em>";
        if (minutes < 10) text += "0";
        text += minutes;
        text += ":";
        if (seconds < 10) text += "0";
        text += seconds;

        tmp.text = text;
    }
}
