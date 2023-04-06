using UnityEngine;
using UnityEditor;
using Enums;
using Unity.VisualScripting;
using System.Reflection.Emit;
using System;

[CustomEditor(typeof(Level))]
public class CustomEditorBoard : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        Level myClass = serializedObject.targetObject as Level;

        EditorGUILayout.BeginHorizontal();
        for (int j = 0; j < 9 ; j++)
        {
            EditorGUILayout.BeginVertical();
            for (int i = 8; i >= 0; i--)
            {
                //GUI.backgroundColor = Color.yellow;
                EditorGUILayout.LabelField(i + " , " + j , GUILayout.Width(45));
                myClass.columns[j].rows[i] = (CellCharacteristic)EditorGUILayout.EnumPopup(myClass.columns[j].rows[i], GUILayout.Width(45));
                myClass.values[j].values[i] =  EditorGUILayout.TextField(myClass.values[j].values[i]);
            }
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndHorizontal();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myClass);
            AssetDatabase.SaveAssets();
        }
    }
}