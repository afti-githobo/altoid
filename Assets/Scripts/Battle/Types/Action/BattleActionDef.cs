using System;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Types.Action
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
            public IReadOnlyList<float> ParamsFloat { get => _paramsFloat; }
            [SerializeField]
            private float[] _paramsFloat;
            public IReadOnlyList<int> ParamsInt { get => _paramsInt; }
            [SerializeField]
            private int[] _paramsInt;
            public IReadOnlyList<string> ParamsString { get => _paramsString; }
            [SerializeField]
            private string[] _paramsString;
            public IReadOnlyList<TextAsset> Scripts { get => _scripts; }
            [SerializeField]
            private TextAsset[] _scripts;
        }

        public string Notes { get => _notes; }
        [SerializeField]
        private string _notes;

        public IReadOnlyList<Packet> Packets { get => _packets; }
        [SerializeField]
        private Packet[] _packets;

        public int ActorEntropy { get => _actorEntropy; }
        [SerializeField]
        private int _actorEntropy;
    }
}