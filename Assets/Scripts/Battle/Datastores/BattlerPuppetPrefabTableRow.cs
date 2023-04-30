using UnityEngine;

namespace Altoid.Battle.Datastores
{
    public class BattlerPuppetPrefabTableRow : ScriptableObject
    {
        [SerializeField]
        private string prefabAssetPath;
        public string PrefabAssetPath { get => prefabAssetPath; }
    }
}