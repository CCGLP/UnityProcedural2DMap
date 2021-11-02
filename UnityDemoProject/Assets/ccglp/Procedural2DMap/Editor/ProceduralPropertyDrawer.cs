using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ccglp.Procedural
{
    [CustomEditor(typeof(ProceduralMap))]
    public class ProceduralPropertyDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ProceduralMap map = (ProceduralMap)target;
            if (GUILayout.Button("Generate"))
            {
                map.RemoveMap();
                map.InitiliazeAndGenerateMap();
            }

        }

    }
}