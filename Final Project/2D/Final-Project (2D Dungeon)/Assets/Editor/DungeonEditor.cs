using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbstractDungeon), true)]
public class DungeonEditor : Editor
{
    AbstractDungeon generator;

    private void Awake()
    {
        generator = (AbstractDungeon)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Create Dungeon"))
        {
            generator.Generate();
        }
    }
}
