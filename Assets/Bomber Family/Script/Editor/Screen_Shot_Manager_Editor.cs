using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Screen_Shot_Manager))]
public class Screen_Shot_Manager_Editor : Editor
{
    #region Property
    private Screen_Shot_Manager ssm;
    [Header("Genel")]
    private SerializedProperty myCamera;
    private SerializedProperty fileName;

    [Header("SS bölgesi")]
    [Tooltip("SS'den kesilip icon olarak kullanılacak bölge")]
    private SerializedProperty textureStartWidth;
    private SerializedProperty textureFinishWidth;
    private SerializedProperty textureStartHeight;
    private SerializedProperty textureFinishHeight;

    [Header("Icon yapılacak objeler")]
    [Tooltip("İlk objenin yeri kameraya göre düzgün ayarlanmalı.")]
    private SerializedProperty allObjects;
    private void OnEnable()
    {
        Serialized();
    }
    public virtual void Serialized()
    {
        ssm = (Screen_Shot_Manager)target;
        // Genel
        myCamera = serializedObject.FindProperty("myCamera");
        fileName = serializedObject.FindProperty("fileName");

        // SS bölgesi
        textureStartWidth = serializedObject.FindProperty("textureStartWidth");
        textureFinishWidth = serializedObject.FindProperty("textureFinishWidth");
        textureStartHeight = serializedObject.FindProperty("textureStartHeight");
        textureFinishHeight = serializedObject.FindProperty("textureFinishHeight");

        // Icon yapılacak objeler
        allObjects = serializedObject.FindProperty("allObjects");
    }
    #endregion
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (GUILayout.Button("Delete First Object"))
        {
            ssm.DeleteFirstObject();
        }
        // Genel
        EditorGUILayout.PropertyField(myCamera);
        EditorGUILayout.PropertyField(fileName);

        // SS bölgesi
        EditorGUILayout.PropertyField(textureStartWidth);
        EditorGUILayout.PropertyField(textureFinishWidth);
        EditorGUILayout.PropertyField(textureStartHeight);
        EditorGUILayout.PropertyField(textureFinishHeight);

        // Icon yapılacak objeler
        EditorGUILayout.PropertyField(allObjects);

        if (GUILayout.Button("Set False All Object"))
        {
            ssm.SetFalseAllObject();
        }
        serializedObject.ApplyModifiedProperties();
    }
}