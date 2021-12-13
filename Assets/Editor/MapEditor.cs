using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// refresh the game while change the setting on Map Generator
[CustomEditor (typeof(MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator map = target as MapGenerator;
        
        if(DrawDefaultInspector())
        {
            map.generateMap();
        }

        // create a button which can manually ganerate map
        if(GUILayout.Button("Generate Map"))
        {
            map.generateMap();
        }
    }
}
