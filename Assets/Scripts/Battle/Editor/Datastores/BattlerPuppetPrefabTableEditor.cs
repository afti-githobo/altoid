using Altoid.Battle.Datastores;
using Altoid.Battle.Frontend;
using Altoid.Battle.Types;
using SolidUtilities.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Altoid.Battle.EditorTools.Datastores
{

    [CustomEditor(typeof(BattlerPuppetPrefabTable))]
    public class BattlerPuppetPrefabTableEditor : Editor
    {
        // TODO: need custom serializer for the dict - that's why this isn't working
        public override void OnInspectorGUI()
        {
            var table = serializedObject.targetObject as BattlerPuppetPrefabTable;
            var data = ((SerializableDictionary<BattlerAssetType, BattlerPuppetPrefabTableRow>)Util.GetPrivate(table, "data"));
            foreach (var k in table.Data.Keys)
            {
                using (var v = new EditorGUILayout.VerticalScope("box"))
                {
                    BattlerAssetType enumVal;
                    bool remove = false;
                    using (var h = new EditorGUILayout.HorizontalScope())
                    {
                        enumVal = (BattlerAssetType)EditorGUILayout.EnumPopup("AssetType", k);
                        remove = GUILayout.Button("-");
                    }
                    var obj = EditorGUILayout.ObjectField("Prefab", table.Data[k].Prefab, typeof(BattlerPuppet), allowSceneObjects: false);
                    if (remove)
                    {
                        DestroyImmediate(table.Data[k]);
                        data.Remove(k);
                        EditorUtility.SetDirty(table.Data[k]);
                        EditorUtility.SetDirty(table);
                        break;
                    }
                    else if (obj != table.Data[k].Prefab)
                    {
                        Util.SetPrivate(table.Data[k], "prefab", obj);
                        EditorUtility.SetDirty(table.Data[k]);
                        EditorUtility.SetDirty(table);
                    }
                    else if (enumVal != k)
                    {

                        if (data.ContainsKey(enumVal)) EditorGUILayout.HelpBox(new GUIContent($"There's already an entry for {enumVal} - can't change the BattlerAssetType associated with this row to that"));
                        else
                        {
                            var row = table.Data[k];
                            data.Remove(k);
                            data.Add(enumVal, row);
                            EditorUtility.SetDirty(table);
                            break;
                        }
                    }
                }
            }
            var addRow = (BattlerAssetType)EditorGUILayout.EnumPopup("Add row for BattlerAssetType of: ", BattlerAssetType.None);
            if (addRow > BattlerAssetType.None)
            {
                if (data.ContainsKey(addRow)) EditorGUILayout.HelpBox(new GUIContent($"There's already an entry for {addRow} - can't add a new row for that BattlerAssetType"));
                data.Add(addRow, CreateInstance(typeof(BattlerPuppetPrefabTableRow)) as BattlerPuppetPrefabTableRow);
                AssetDatabase.AddObjectToAsset(data[addRow], serializedObject.targetObject);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.SetDirty(data[addRow]);
                EditorUtility.SetDirty(table);
            }
        }
    }
}