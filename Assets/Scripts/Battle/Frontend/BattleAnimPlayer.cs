using UnityEngine;

namespace Altoid.Battle.Frontend
{
    public class BattleAnimPlayer : MonoBehaviour
    {
        public static BattleAnimPlayer Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Play(string anim)
        {
            Debug.Log("anim: " + anim);
        }
    }
}