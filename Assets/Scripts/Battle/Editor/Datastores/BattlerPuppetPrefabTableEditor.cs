using Altoid.Battle.Datastores;
using Altoid.Battle.Types.Battlers;
using SolidUtilities.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Altoid.Battle.EditorTools.Datastores
{

    [CustomEditor(typeof(BattlerPuppetPrefabTable))]
    public class BattlerPuppetPrefabTableEditor : Editor
    {
        Dictionary<BattlerAssetType, ResourceRequest> loadedPrefabsDict = new();

        public override void OnInspectorGUI()
        {
            var table = serializedObject.targetObject as BattlerPuppetPrefabTable;
            var data = ((SerializableDictionary<BattlerAssetType, BattlerPuppetPrefabTableRow>)Util.GetPrivate(table, "data"));

            foreach (var k in table.Data.Keys)
            {
                bool changedObject = false;
                Object changedObjectReference = null;
                using (var v = new EditorGUILayout.VerticalScope("box"))
                {
                    BattlerAssetType enumVal;
                    bool remove = false;
                    using (var h = new EditorGUILayout.HorizontalScope())
                    {
                        enumVal = (BattlerAssetType)EditorGUILayout.EnumPopup("AssetType", k);
                        remove = GUILayout.Button("-", GUILayout.Width(16));
                    }
                    using (var h = new EditorGUILayout.HorizontalScope())
                    {
                        if (!loadedPrefabsDict.ContainsKey(k)) loadedPrefabsDict.Add(k, Resources.LoadAsync(table.Data[k].PrefabAssetPath));
                        if (!loadedPrefabsDict[k].isDone) EditorGUILayout.LabelField("Prefab", "Loading...");
                        else
                        {
                            changedObjectReference = EditorGUILayout.ObjectField("Prefab", loadedPrefabsDict[k].asset, typeof(GameObject), allowSceneObjects: false);
                            changedObject = changedObjectReference != loadedPrefabsDict[k].asset;
                        }
                    }
                    if (remove)
                    {
                        DestroyImmediate(table.Data[k], true);
                        data.Remove(k);
                        loadedPrefabsDict.Remove(k);
                        EditorUtility.SetDirty(table);
                        break;
                    }
                    else if (changedObject)
                    {
                        Util.SetPrivate(table.Data[k], "prefabAssetPath", AssetDatabase.GetAssetPath(changedObjectReference).Replace("Assets/Resources/", "").Replace(".prefab", ""));
                        EditorUtility.SetDirty(table.Data[k]);
                        EditorUtility.SetDirty(table);
                        loadedPrefabsDict[k] = Resources.LoadAsync(table.Data[k].PrefabAssetPath);
                    }
                    else if (enumVal != k)
                    {
                        if (data.ContainsKey(enumVal)) Debug.LogError($"There's already an entry for {enumVal} - can't change the BattlerAssetType associated with this row to that");
                        else
                        {
                            var row = table.Data[k];
                            loadedPrefabsDict[enumVal] = loadedPrefabsDict[k];
                            data.Remove(k);
                            loadedPrefabsDict.Remove(k);
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
                if (data.ContainsKey(addRow)) Debug.LogError($"There's already an entry for {addRow} - can't add a new row for that BattlerAssetType");
                else
                {
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
}