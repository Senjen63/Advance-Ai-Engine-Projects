using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    public ReadCube readCube;
    public CubeState cubeState;
    private bool doOnce = true;

    void Start()
    {
        readCube = FindAnyObjectByType<ReadCube>();
        cubeState = FindAnyObjectByType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CubeState.started && doOnce)
        {
            doOnce = false;
            Solve();
        }
    }

    public void Solve()
    {
        readCube.ReadState();

        //Get the State of the cube as a string
        string moveString = cubeState.GetStsateString();
        print(moveString);

        //Solve the Cube
        string info = "";

        //First time build the tables
        //string solution = SearchRunTime.solution(moveString, out info, buildTables: true);


        //Every other time
        string solution = Search.solution(moveString, out info);

        //Convert the solved moves from a string to a list
        List<string> solutionList = StringToList(solution);

        //Automate the list
        Automate.moveList = solutionList;
        print(info);
    }

    List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
        
        return solutionList;
    }
}
