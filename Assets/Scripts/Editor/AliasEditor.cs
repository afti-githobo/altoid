using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AliasAttribute))]
public class AliasEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, new GUIContent((attribute as AliasAttribute).Name));
    }
}
