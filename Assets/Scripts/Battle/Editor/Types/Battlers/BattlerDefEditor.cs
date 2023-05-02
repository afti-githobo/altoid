using Altoid.Battle.Types.Battlers;
using UnityEditor;
using UnityEngine;

namespace Altoid.Battle.EditorTools.Types.Battlers
{
    [CustomEditor(typeof(BattlerDef))]
    public class BattlerDefEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_description"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_assetType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_identity"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_defaultFaction"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_script"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_actionSource"));
            BaseStatsFields();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_baseLevel"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_tags"));
            // todo: growth data
            // todo: elements
            if (EditorGUI.EndChangeCheck()) serializedObject.ApplyModifiedProperties();
        }

        private void BaseStatsFields()
        {
            var prop = serializedObject.FindProperty("_baseStats");
            if (prop.arraySize != Constants.NUM_STATS) prop.arraySize = Constants.NUM_STATS;
            EditorGUILayout.LabelField("Base Stats");
            using (var v = new GUILayout.VerticalScope("box"))
            {
                EditorGUILayout.PropertyField(prop.GetArrayElementAtIndex(0), new GUIContent("Max HP"));
                EditorGUILayout.PropertyField(prop.GetArrayElementAtIndex(1), new GUIContent("ATK"));
                EditorGUILayout.PropertyField(prop.GetArrayElementAtIndex(2), new GUIContent("DEF"));
                EditorGUILayout.PropertyField(prop.GetArrayElementAtIndex(3), new GUIContent("DEX"));
                EditorGUILayout.PropertyField(prop.GetArrayElementAtIndex(4), new GUIContent("AGI"));
                EditorGUILayout.PropertyField(prop.GetArrayElementAtIndex(5), new GUIContent("SPD"));
            }
            
        }
    }
}