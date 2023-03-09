using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public GameObject obj;
    float speed = 200.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();

        //Automatically move to the target position
        if(transform.rotation != obj.transform.rotation)
        {
            var step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, obj.transform.rotation, step);
        }
    }

    void Swipe()
    {
        if(Input.GetMouseButtonDown(1))
        {
            //Get the 2D position of the first mouse click
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if(Input.GetMouseButtonUp(1))
        {
            //Get the 2D position of the second mouse click
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //Create a vector from the first and second click positions
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //Normalizes the 2D vector
            currentSwipe.Normalize();

            //When swipe
            if(LeftSwipe(currentSwipe))
            {
                obj.transform.Rotate(0, 90, 0, Space.World);
            }

            else if(RightSwipe(currentSwipe))
            {
                obj.transform.Rotate(0, -90, 0, Space.World);
            }
        }
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }
}
