using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    public Material[] materials;
    public int index = 0;
    
    void Start()
    {
        materials = new Material[index];
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            index++;
        }
    }
}
