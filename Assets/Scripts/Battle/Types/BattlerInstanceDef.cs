using System;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Types
{
    [Serializable]
    public struct BattlerInstanceDef
    {
        public BattlerDef Battler { get => _battler; }
        [SerializeField]
        private BattlerDef _battler;
        public BattleFaction Faction { get => _faction; }
        [SerializeField]
        private BattleFaction _faction;
        public int LevelBonus { get => _levelBonus; }
        [SerializeField]
        private int _levelBonus;
        public StanceType StartingStance { get => _startingStance; }
        [SerializeField]
        private StanceType _startingStance;
        public int StartingDelay { get => _startingDelay; }
        [SerializeField]
        private int _startingDelay;
        public int StartingEntropy { get => _startingEntropy; }
        [SerializeField]
        private int _startingEntropy;
        public int StartingDamage { get => _startingDamage; }
        [SerializeField]
        private int _startingDamage;
        public Vector3 Position { get => _position; }
        [SerializeField]
        private Vector3 _position;
        public float Angle { get => _angle; }
        [SerializeField]
        private float _angle;
        public bool Hidden { get => _hidden; }
        [SerializeField]
        private bool _hidden;
        public bool Dead { get => _dead; }
        [SerializeField]
        private bool _dead;
        public IReadOnlyList<string> Tags { get => _tags; }
        [SerializeField]
        private List<string> _tags;

        public BattlerInstanceDef(BattlerDef battler, BattleFaction faction, int levelBonus, StanceType startingStance, int startingEntropy, int startingDamage, int startingDelay, Vector3 position, float angle, bool hidden, bool dead, List<string> tags)
        {
            _battler = battler;
            _faction = faction;
            _levelBonus = levelBonus;
            _startingStance = startingStance;
            _startingEntropy = startingEntropy;
            _startingDamage = startingDamage;
            _startingDelay = startingDelay;
            _position = position;
            _angle = angle;
            _hidden = hidden;
            _dead = dead;
            _tags = tags;
        }
    }
}