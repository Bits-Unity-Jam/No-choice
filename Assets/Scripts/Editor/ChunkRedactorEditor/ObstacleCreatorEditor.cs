using System;
using Chunks.ChunkRedactor;
using UnityEditor;
using UnityEngine;

namespace Editor.ChunkRedactorEditor
{
    [CustomEditor(typeof(ObstacleCreator))]
    public class ObstacleCreatorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            Color starColor = GUI.backgroundColor;
            
            ObstacleCreator obstacleCreator = target as ObstacleCreator;
            
            int obstacleCount = Enum.GetNames(typeof(ObstacleId)).Length;
            
            GUILayout.BeginVertical();
            GUI.backgroundColor = Color.green;
            for (int i = 0; i < obstacleCount; i++)
            {
                if (GUILayout.Button($"Add {Enum.GetNames(typeof(ObstacleId))[i]}",GUILayout.Height(40f)))
                {
                    obstacleCreator.Create((ObstacleId)i);
                }
            }
            
            GUILayout.EndVertical();
            
            GUILayout.Space(10f);
            
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Clear",GUILayout.Height(40f)))
            {
                obstacleCreator.Clear();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(20f);
            
            GUI.backgroundColor = starColor;
            DrawDefaultInspector();
        }
    }
    
    [CustomEditor(typeof(ChunkBuilder))]
    public class ChunkBuilderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            GUILayout.Space(20f);
            ChunkBuilder obstacleCreator = target as ChunkBuilder;
            
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Save",GUILayout.Height(40f)))
            {
                obstacleCreator.Save();
            }
            GUI.backgroundColor = Color.yellow;
            if (GUILayout.Button("Load",GUILayout.Height(40f)))
            {
                obstacleCreator.Load();
            }
            GUILayout.EndHorizontal();
        }
    }
}