using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Altoid.Battle.EditorTools
{
    public static class Util
    {
        public static object GetPrivate<T>(T obj, string name) => typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
        public static void SetPrivate<T>(T obj, string name, object value) => typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(obj, value);

        public static void TextProp(UnityEngine.Object obj, string backingField, string value, GUIContent label)
        {
            var field = obj.GetType().GetField(backingField, BindingFlags.NonPublic | BindingFlags.Instance);
            var data = EditorGUILayout.TextField(label, value);
            if (data != value)
            {
                field.SetValue(obj, data);
                EditorUtility.SetDirty(obj);
            }
        }

        public static void ObjectProp<T>(UnityEngine.Object obj, string backingField, UnityEngine.Object value, GUIContent label, bool allowSceneObjects) where T: UnityEngine.Object
        {
            var field = obj.GetType().GetField(backingField, BindingFlags.NonPublic | BindingFlags.Instance);
            var data = EditorGUILayout.ObjectField(label, value, typeof(T), allowSceneObjects);
            if (data != value)
            {
                field.SetValue(obj, data);
                EditorUtility.SetDirty(obj);
            }
        }
    }
}