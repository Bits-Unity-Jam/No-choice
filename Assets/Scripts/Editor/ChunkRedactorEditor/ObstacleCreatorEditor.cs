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
            DrawDefaultInspector();
            
            ObstacleCreator obstacleCreator = target as ObstacleCreator;
            
            int obstacleCount = Enum.GetNames(typeof(ObstacleId)).Length;
            
            for (int i = 0; i < obstacleCount; i++)
            {
                if (GUILayout.Button($"Add {Enum.GetNames(typeof(ObstacleId))[i]}"))
                {
                    obstacleCreator.Create((ObstacleId)i);
                }
            }
        }
    }
}