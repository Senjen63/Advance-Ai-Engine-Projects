using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;

public class Manager : MonoBehaviour
{
    public bool isPaused = true;
    public Game game;
    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void StepGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        game.timer = game.speed;
        game.Update();
        Time.timeScale = 0;
    }

    public void RunGame()
    {

        isPaused = false;
       Time.timeScale = 1; 
    }

    public void DrawGame()
    {
        game.PlaceCells();
    }

    
}
