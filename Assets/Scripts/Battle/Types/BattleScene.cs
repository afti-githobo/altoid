using UnityEngine;

namespace Altoid.Battle.Types
{
    [CreateAssetMenu(fileName = "New Battle Scene", menuName = "Altoid/Battle/Battle Scene", order = 3)]
    public class BattleScene : ScriptableObject
    {
        public string Description { get => _description; }
        [SerializeField]
        private string _description;
        public int SceneIndex { get => _sceneIndex; }
        [SerializeField]
        private int _sceneIndex;
        public PolyScript Script { get => _script; }
        [SerializeField]
        private PolyScript _script;
    }
}