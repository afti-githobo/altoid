using System;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Util
{
    public abstract class Datastore : ScriptableObject
    {
        private static readonly Dictionary<Type, Datastore> instancesDict = new();

        private static T GetInstance<T>() where T : Datastore
        {
            var vs = Resources.FindObjectsOfTypeAll<T>();
            if (vs.Length > 1) throw new Exception($"Multiple instances of datastore ScriptableObject type {typeof(T).Name} exist in the Resources folder. " +
                $"Datastore ScriptableObjects should have only a single instance!");
            else if (vs.Length == 0) throw new Exception($"No instance of datastore ScriptableObject type {typeof(T).Name} exists in the Resources folder.");
            return vs[0];
        }

        public static T Query<T>() where T : Datastore
        {
            if (!instancesDict.ContainsKey(typeof(T))) instancesDict[typeof(T)] = GetInstance<T>();
            return instancesDict[typeof(T)] as T;
        }
    }
}