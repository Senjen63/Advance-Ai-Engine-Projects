using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public float speed;
    public Text speedText;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.transform.position;
        float size = cam.orthographicSize;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            size += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            size -= speed * Time.deltaTime;
        }

        cam.transform.position = pos;
        cam.orthographicSize = size;

        speedText.text = "Camera Speed: " + speed.ToString();

    }

    public void SpeedUp()
    {
        speed++;
    }

    public void SlowDown()
    {
        speed--;

        if(speed <= 0)
        {
            speed = 0;
        }
    }
}
