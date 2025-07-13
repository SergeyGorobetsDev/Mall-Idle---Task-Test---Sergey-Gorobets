using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(InteriorObjectsHandler))]
[CanEditMultipleObjects]
public class InteriorObjectsHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var handler = (InteriorObjectsHandler)target;
        var dict = handler.InteriorObjects;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Interior Objects Dictionary", EditorStyles.boldLabel);

        if (dict == null || dict.Count == 0)
        {
            EditorGUILayout.LabelField("Dictionary is empty.");
            return;
        }

        foreach (KeyValuePair<InteriorType, List<InteriorEntity>> kvp in dict)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Type: {kvp.Key} ({kvp.Value.Count})", EditorStyles.boldLabel);

            if (kvp.Value != null && kvp.Value.Count > 0)
            {
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    var entity = kvp.Value[i];
                    EditorGUILayout.ObjectField($"[{i}]", entity, typeof(InteriorEntity), true);
                }
            }
            else
            {
                EditorGUILayout.LabelField("No entities.");
            }

            EditorGUILayout.EndVertical();
        }
    }
}