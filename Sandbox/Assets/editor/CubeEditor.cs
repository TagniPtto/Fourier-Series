using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cube))]
public class CubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Cube cube = (Cube)target;

        if (GUILayout.Button("Generate"))
        {
           cube.GenerateTerrain();
        }
    }
}
