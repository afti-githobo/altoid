using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Frontend
{
    public class BattlerPuppet : MonoBehaviour
    {
        public readonly struct DamageEvent
        {
            public readonly int Damage;
            public readonly bool Miss;

            public DamageEvent(int damage, bool miss)
            {
                Damage = damage;
                Miss = miss;
            }
        }

        private BattlerHudHPBar hpBar;
        private Queue<DamageEvent> stagedDamageEvents = new();
        public bool HasStagedDamageEvents { get => stagedDamageEvents.Count > 0; }

        public void AssignHPBar(BattlerHudHPBar bar) => hpBar = bar;

        public void StageDamage(int dmg)
        {
            stagedDamage.Enqueue(dmg);
        }

        public void TakeStagedDamage()
        {
            var dmg = stagedDamage.Dequeue();
            EmitDamageNumber(dmg);
            hpBar?.Hurt(dmg);
        }

        private void EmitDamageNumber(int dmg)
        {
            Debug.Log(dmg);
        }
    }
}