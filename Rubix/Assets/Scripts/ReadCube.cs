using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;
    private int layerMask = 1 << 8; //Faces of the cube
    CubeState cubeState;
    CubeMap cubeMap;

    void Start()
    {
        cubeState = FindAnyObjectByType<CubeState>();
        cubeMap= FindAnyObjectByType<CubeMap>();

        List<GameObject> facesHit = new List<GameObject>();
        Vector3 ray = front.transform.position;
        RaycastHit hit;

        //Does the ray intersect any objects in the layerMask?
        if (Physics.Raycast(ray, front.right, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(ray, front.right * hit.distance, Color.yellow);
            facesHit.Add(hit.collider.gameObject);
            print(hit.collider.gameObject);
        }

        else
        {
            Debug.DrawRay(ray, front.right * 1000, Color.green);
        }

        cubeState.front = facesHit;
        cubeMap.Set();
    }

    
    void Update()
    {
        
    }

    //List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    //{

    //}
}
