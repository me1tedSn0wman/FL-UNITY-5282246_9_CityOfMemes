using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
public class SaveManagerEditor : Editor
{
    private SaveManager saveManager;

    SerializedProperty serProp_saveableObjectDefs;
    SerializedProperty serProp_dataOverall;

    private void OnEnable()
    {
        saveManager = (SaveManager)target;

        serProp_saveableObjectDefs = serializedObject.FindProperty("saveableObjectDefs");
        serProp_dataOverall = serializedObject.FindProperty("dataOverall");
    }

    public override void OnInspectorGUI() { 
        serializedObject.Update();

        EditorGUILayout.PropertyField(serProp_saveableObjectDefs);
        EditorGUILayout.PropertyField(serProp_dataOverall);

        if (GUILayout.Button("Generate List For Save"))
        {
            saveManager.GenerateListOfObjects();
            EditorUtility.SetDirty(saveManager);
            Undo.RecordObject(saveManager, "Save Remap");
            //            serializedObject.ApplyModifiedProperties();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
