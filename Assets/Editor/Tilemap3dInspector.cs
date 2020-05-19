using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tilemap3d))]
public class Tilemap3dInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate Tilemap"))
        {
            Tilemap3d tilemap3d = (Tilemap3d)target;
            tilemap3d.BuildMesh();
        }
    }
}
