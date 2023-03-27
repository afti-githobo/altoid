using Altoid.Battle.Logic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Data.Action
{
    [CreateAssetMenu(fileName = "New Battle Action Definition", menuName = "Altoid/Battle/Actions/Battle Action Definition", order = 0)]
    public class BattleActionDef : ScriptableObject
    {
        [Serializable]
        public class Packet
        {
            public string Notes { get => _notes; }
            [SerializeField]
            private string _notes;
            public IReadOnlyList<int> TargetGroups { get => _targetGroups; }
            [SerializeField]
            private int[] _targetGroups;
            public IReadOnlyList<BattleSignal> Signals { get => _signals; }
            [SerializeField]
            private BattleSignal[] _signals;
            public IReadOnlyList<float> ParamsFloat { get => _paramsFloat; }
            [SerializeField]
            private float[] _paramsFloat;
            public IReadOnlyList<int> ParamsInt { get => _paramsInt; }
            [SerializeField]
            private int[] _paramsInt;
            public IReadOnlyList<int> ParamsString { get => _paramsString; }
            [SerializeField]
            private int[] _paramsString;
            public IReadOnlyList<BattleScript> Scripts { get => _scripts; }
            [SerializeField]
            private BattleScript[] _scripts;
        }

        public string Notes { get => _notes; }
        [SerializeField]
        private string _notes;

        public IReadOnlyList<Packet> Packets { get => _packets; }
        [SerializeField]
        private Packet[] _packets;
    }
}