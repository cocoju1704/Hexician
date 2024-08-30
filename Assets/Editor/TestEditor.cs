using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CustomEditor(typeof(Stage))]
public class TestEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

		if (GUILayout.Button("QQQQ"))
		{}
    }

}
