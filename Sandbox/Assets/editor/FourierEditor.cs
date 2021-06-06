using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Fourier))]
public class FourierEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Fourier f = (Fourier)target;

        if (GUILayout.Button("Reset Coefficient"))
        {
            f.FoSe();
        }
        if (GUILayout.Button("Generate"))
        {
            f.Generate();
        }
    }
}
