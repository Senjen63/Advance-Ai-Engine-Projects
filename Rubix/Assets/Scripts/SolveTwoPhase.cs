using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    public ReadCube readCube;
    public CubeState cubeState;

    void Start()
    {
        readCube = FindAnyObjectByType<ReadCube>();
        cubeState = FindAnyObjectByType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Solve()
    {
        readCube.ReadState();

        //Get the State of the cube as a string

        //Solve the Cube

        //Convert the solved moves from a string to a list

        //Automate the list
    }
}
