using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    public Material[] materials;
    private int index = 0;
    private Renderer backgroundRenderer;

    void Start()
    {
        // Get the renderer component of the background object
        backgroundRenderer = GetComponent<Renderer>();

        // Set the initial material
        backgroundRenderer.material = materials[index];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Increment the index to change to the next material
            index = (index + 1) % materials.Length;

            // Update the material
            backgroundRenderer.material = materials[index];
        }
    }
}