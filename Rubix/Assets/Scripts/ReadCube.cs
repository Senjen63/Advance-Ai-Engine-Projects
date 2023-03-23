using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;
    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();

    //Faces of the cube
    private int layerMask = 1 << 8;

    CubeState cubeState;
    CubeMap cubeMap;
    public GameObject emptyGO;

    void Start()
    {
        SetRayTransform();

        cubeState = FindAnyObjectByType<CubeState>();
        cubeMap = FindAnyObjectByType<CubeMap>();
        ReadState();
        CubeState.started = true;
    }

    public void ReadState()
    {
        cubeState = FindAnyObjectByType<CubeState>();
        cubeMap = FindAnyObjectByType<CubeMap>();

        //Set the state of each position in the list of sides so we know what color is in what position
        cubeState.up = ReadFace(upRays, up);
        cubeState.down = ReadFace(downRays, down);
        cubeState.left = ReadFace(leftRays, left);
        cubeState.right = ReadFace(rightRays, right);
        cubeState.front = ReadFace(frontRays, front);
        cubeState.back = ReadFace(backRays, back);

        //Update the map with the found positions
        cubeMap.Set();
    }

    void SetRayTransform()
    {
        //Populate the ray lists with RayCasts eminating from the transform, angled towards the cube
        upRays = BuildRays(up, new Vector3(90, 90, 0));
        downRays = BuildRays(down, new Vector3(270, 90, 0));
        leftRays = BuildRays(left, new Vector3(0, 180, 0));
        rightRays = BuildRays(right, new Vector3(0, 0, 0));
        frontRays = BuildRays(front, new Vector3(0, 90, 0));
        backRays = BuildRays(back, new Vector3(0, 270, 0));
    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        //The Ray count is used to name the rays so we can be sure they are in the right order
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        //This creates 9 rays in the  shape of the side of the cube with Ray 0 at the top left and ray 8 at the bottom right:
        //|0|1|2|
        //|3|4|5|
        //|6|7|8|

        for(int y = 1; y > -2; y--)
        {
            for(int x = -1; x < 2; x++)
            {
                Vector3 startpos = new Vector3(rayTransform.localPosition.x + x,
                                               rayTransform.localPosition.y + y, 
                                               rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startpos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();

        foreach(GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            //Does the ray intersect any objects in the layerMask?
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
            }

            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }        

        return facesHit;
    }
}
