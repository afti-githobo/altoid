using Altoid.Battle.Types.Battlers;
using Altoid.Util;
using SolidUtilities.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Datastores
{
    [CreateAssetMenu(fileName = "[MASTER] BattlerPuppetPrefabTable", menuName = "Altoid/Battle/! DATASTORE ! Don't make duplicates/BattlerPuppetPrefabTable", order = 0)]
    public class BattlerPuppetPrefabTable : Datastore
    {
        [SerializeField]
        private SerializableDictionary<BattlerAssetType, BattlerPuppetPrefabTableRow> data = new();
        public IReadOnlyDictionary<BattlerAssetType, BattlerPuppetPrefabTableRow> Data { get => data; }
    }
}