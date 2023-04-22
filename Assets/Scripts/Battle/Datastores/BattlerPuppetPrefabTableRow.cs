using Altoid.Battle.Frontend;
using UnityEngine;

namespace Altoid.Battle.Datastores
{
    public class BattlerPuppetPrefabTableRow : ScriptableObject
    {
        [SerializeField]
        private BattlerPuppet prefab;
        public BattlerPuppet Prefab { get => prefab; }
    }
}