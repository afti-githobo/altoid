using Altoid.Battle.Types.Battle;
using Altoid.Battle.Types.Battlers.AI;
using System;
using System.Collections.Generic;
using TypeReferences;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Altoid.Battle.Types.Battlers
{
    [CreateAssetMenu(fileName = "New Battler Definition", menuName = "Altoid/Battle/Battler Definition", order = 2)]
    public class BattlerDef : ScriptableObject
    {
        public string Description { get => _description; }
        [SerializeField]
        private string _description;
        public BattlerAssetType AssetType { get => _assetType; }
        [SerializeField]
        private BattlerAssetType _assetType;
        public BattlerIdentity Identity { get => _identity; }
        [SerializeField]
        private BattlerIdentity _identity;
        public BattleFaction DefaultFaction { get => _defaultFaction; }
        [SerializeField]
        private BattleFaction _defaultFaction;
        public PolyScript Script { get => _script; }
        [SerializeField]
        private PolyScript _script;
        public int BaseLevel { get => _baseLevel; }
        [SerializeField]
        private int _baseLevel;
        public int[] BaseStats { get => _baseStats; }
        [SerializeField]
        private int[] _baseStats;
        public BattlerGrowthData GrowthData { get => _growthData; }
        [SerializeField]
        private BattlerGrowthData _growthData;
        public BattlerElementParams ElementParams { get => _elementParams; }
        [SerializeField]
        private BattlerElementParams _elementParams;
        public IReadOnlyList<string> Tags { get => _tags; }
        [SerializeField]
        private List<string> _tags = new();

        public TypeReference ActionSource { get => _actionSource; }
        [SerializeField]
        [Inherits(typeof(ActionSource))]
        private TypeReference _actionSource;

#if UNITY_EDITOR
        private void OnValidate()
        {
            var assetPath = AssetDatabase.GetAssetPath(this);
            if (_elementParams == null || _growthData == null)
            {
                _growthData = CreateInstance<BattlerGrowthData>();
                _elementParams = CreateInstance<BattlerElementParams>();
                AssetDatabase.AddObjectToAsset(_growthData, assetPath);
                AssetDatabase.AddObjectToAsset(_elementParams, assetPath);
                AssetDatabase.SaveAssets();
            }
        }
#endif
    }
}