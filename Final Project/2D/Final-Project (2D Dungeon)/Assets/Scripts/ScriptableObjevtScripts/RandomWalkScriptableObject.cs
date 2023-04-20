using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//referenced tutorial
[CreateAssetMenu(fileName = "RandomWalkParameters", menuName = "Procedural Content Generator/RandomWalkScriptableObject")]
public class RandomWalkScriptableObject : ScriptableObject
{
    public int iterations = 10;
    public int walkLength = 10;
    public bool StartRandomlyIterating = true;
}
