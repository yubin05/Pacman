using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GoogleSheetLoader))]
public class GoogleSheetLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (GoogleSheetLoader)target;

        if (GUILayout.Button("Open Sheet"))
        {
            script.OpenSheet();
        }
        if (GUILayout.Button("Refresh Sheet"))
        {
            script.RefreshSheet(); // test code
        }
    }
}
