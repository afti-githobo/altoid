using UnityEditor;
using UnityEngine;

namespace Altoid.Battle.EditorTools
{
    public static class Util
    {
        public static void TextProp(Object obj, string backingField, string value, GUIContent label)
        {
            var field = obj.GetType().GetField(backingField, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var data = EditorGUILayout.TextField(label, value);
            if (data != value)
            {
                field.SetValue(obj, data);
                EditorUtility.SetDirty(obj);
            }
        }

        public static void ObjectProp<T>(Object obj, string backingField, Object value, GUIContent label, bool allowSceneObjects) where T: Object
        {
            var field = obj.GetType().GetField(backingField, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var data = EditorGUILayout.ObjectField(label, value, typeof(T), allowSceneObjects);
            if (data != value)
            {
                field.SetValue(obj, data);
                EditorUtility.SetDirty(obj);
            }
        }
    }
}