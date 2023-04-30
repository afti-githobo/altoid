using UnityEngine;
using System.Collections.Generic;

namespace Altoid.Battle.Types.Battlers
{
    public class BattlerGrowthData : ScriptableObject
    {
        public string Description { get => _description; }
        [SerializeField]
        private string _description;
        public IReadOnlyList<int> MaxHPGrowthVals { get => _maxHpGrowthVals; }
        [SerializeField]
        private int[] _maxHpGrowthVals;
        public IReadOnlyList<int> AttackGrowthVals { get => _attackGrowthVals; }
        [SerializeField]
        private int[] _attackGrowthVals;
        public IReadOnlyList<int> DefenseGrowthVals { get => _defenseGrowthVals; }
        [SerializeField]
        private int[] _defenseGrowthVals;
        public IReadOnlyList<int> DexterityGrowthVals { get => _dexterityGrowthVals; }
        [SerializeField]
        private int[] _dexterityGrowthVals;
        public IReadOnlyList<int> AgilityGrowthVals { get => _agilityGrowthVals; }
        [SerializeField]
        private int[] _agilityGrowthVals;
        public IReadOnlyList<int> SpeedGrowthVals { get => _speedGrowthVals; }
        [SerializeField]
        private int[] _speedGrowthVals;
    }
}