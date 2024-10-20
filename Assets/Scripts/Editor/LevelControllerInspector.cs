using Controllers;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelController))]
public class LevelControllerInspector : Editor
{
    private LevelController _levelController;
    private int _level;

    private void OnEnable()
    {
        _levelController = target as LevelController;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _level = EditorGUILayout.IntField("Level", _level);
        if (GUILayout.Button("SetLevel"))
        {
            _levelController.SetLevel(_level);
        }
    }
}