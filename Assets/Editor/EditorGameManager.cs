using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class EditorGameManager : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("GetNextMove"))
        {
            Debug.Log(ChessAI.GetNextMove(GameManager.Instance.Board, Color.Black));
        }

        // Show default inspector property editor
        DrawDefaultInspector ();
    }
}
