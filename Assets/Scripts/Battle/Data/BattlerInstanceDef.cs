using System;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Data
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
        public int Level { get => _level; }
        [SerializeField]
        private int _level;
        public Vector3 Position { get => _position; }
        [SerializeField]
        private Vector3 _position;
        public float Angle { get => _angle; }
        [SerializeField]
        private float _angle;
        public IReadOnlyList<string> Tags { get => _tags; }
        [SerializeField]
        private List<string> _tags;

        public BattlerInstanceDef(BattlerDef battler, BattleFaction faction, int level, Vector3 position, float angle, params string[] tags)
        {
            _battler = battler;
            _faction = faction;
            _level = level;
            _position = position;
            _angle = angle;
            _tags = new List<string>(tags);
        }
    }
}