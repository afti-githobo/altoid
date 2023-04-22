using Altoid.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Types
{
    [CreateAssetMenu(fileName = "New Battle Definition", menuName = "Altoid/Battle/Battle Definition", order = 0)]
    public class BattleDef : ScriptableObject
    {
        public string Description { get => _description; }
        [SerializeField]
        [Alias("Description")]
        [Tooltip("Human-readable description - not shown ingame")]
        private string _description;
        public AudioClip BGM { get => _bgm; }
        [SerializeField]
        [Alias("BGM")]
        [Tooltip("Initial background music for the battle")]
        private AudioClip _bgm;
        public PolyScript MainBattleScript { get => _mainBattleScript; }
        [SerializeField]
        [Alias("Main BattleScript")]
        [Tooltip("Main script for the battle")]
        private PolyScript _mainBattleScript;
        public BattleScene InitialBattleScene { get => _initialBattleScene; }
        [SerializeField]
        [Alias("Initial BattleScene")]
        [Tooltip("Starting BattleScene for the battle")]
        private BattleScene _initialBattleScene;
        public IReadOnlyList<BattlerInstanceDef> Battlers { get => _battlers; }
        [SerializeField]
        [Tooltip("Battlers included in this battle")]
        private List<BattlerInstanceDef> _battlers = new();
    }
}