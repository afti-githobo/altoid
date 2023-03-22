using Altoid.Battle.Data;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Altoid.Battle.EditorTools
{
    [CustomEditor(typeof(BattleDef))]
    public class BattleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}