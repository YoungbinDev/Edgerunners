using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameFeature))]
public class GameFeatureEditor : Editor
{
    GameFeatureActionType selectedActionType;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameFeature gameFeature = (GameFeature)target;

        GUILayout.Space(30);

        selectedActionType = (GameFeatureActionType)EditorGUILayout.EnumPopup("Selected Action", selectedActionType);

        if (GUILayout.Button("Add Action"))
        {
            gameFeature.AddAction(selectedActionType);
        }
    }
}
