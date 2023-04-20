using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//referenced tutorial
public abstract class AbstractDungeon : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer;

    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void Generate()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
