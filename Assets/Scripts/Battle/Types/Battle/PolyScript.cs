using Altoid.Battle.Types.Battle;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Types.Battle
{
    [CreateAssetMenu(fileName = "New PolyScript", menuName = "Altoid/Battle/PolyScript", order = 1)]
    public class PolyScript : ScriptableObject
    {
        public string Description { get => _description; }
        [SerializeField]
        private string _description;
        public IReadOnlyDictionary<BattleEventType, TextAsset> ActionTable => _actionTable;
        private Dictionary<BattleEventType, TextAsset> _actionTable = new();
    }
}