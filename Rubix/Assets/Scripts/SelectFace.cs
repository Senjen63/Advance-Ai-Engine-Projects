using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;
    int layerMask = 1 << 8;

    void Start()
    {
        readCube = FindAnyObjectByType<ReadCube>();
        cubeState = FindAnyObjectByType<CubeState>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !CubeState.autoRotating)
        {
            //Read the Current State of the cube
            readCube.ReadState();

            //Raycast from the mouse towards the cube to see if a face is hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;

                //Make a list of all the sides (lists of face GameObjects)
                List<List<GameObject>> cubSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };

                //If the face hit exists within a side
                foreach(List<GameObject> cubeSide in cubSides)
                {
                    if(cubeSide.Contains(face))
                    {
                        //Pick it up
                        cubeState.PickUp(cubeSide);

                        //Start the side rotation logic
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
                    }
                }
            }
        }
    }
}
