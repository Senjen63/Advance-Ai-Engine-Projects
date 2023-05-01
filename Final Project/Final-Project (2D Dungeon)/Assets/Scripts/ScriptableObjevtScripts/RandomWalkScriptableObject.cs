using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Looked at how to video

[CreateAssetMenu(fileName = "RandomWalkParameters", menuName = "Procedural Content Generator/RandomWalkScriptableObject")]
public class RandomWalkScriptableObject : ScriptableObject
{
    public int iterations = 10;
    public int walkLength = 10;
    public bool StartRandomlyIterating = true;
}
