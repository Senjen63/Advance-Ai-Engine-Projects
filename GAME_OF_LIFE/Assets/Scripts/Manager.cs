using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public bool isPaused = false;
    int onOff = 0;
    Button button;
    public void StopGame()
    {
        if(onOff == 0)
        {
            isPaused = false;
        }

        if(onOff == 1)
        {
            isPaused = true;
        }

        if(onOff > 1)
        {
            onOff = 0;
        }

        if (isPaused)
        { 
            Time.timeScale = 0;
            Debug.Log("Stop");
        }

        //if(button.onClick.AddListener()

        


    }
}
