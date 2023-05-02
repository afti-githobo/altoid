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
            public readonly bool Death;

            public DamageEvent(int damage, bool miss, bool death)
            {
                Damage = damage;
                Miss = miss;
                Death = death;
            }
        }

        private BattlerHudHPBar hpBar;
        private Queue<DamageEvent> stagedDamageEvents = new();
        public bool HasStagedDamageEvents { get => stagedDamageEvents.Count > 0; }

        public void AssignHPBar(BattlerHudHPBar bar) => hpBar = bar;

        public void StageDamageEvent(DamageEvent evt)
        {
            stagedDamageEvents.Enqueue(evt);
        }

        public void TakeStagedDamage()
        {
            var evt = stagedDamageEvents.Dequeue();
            if (!evt.Miss)
            {
                EmitDamageNumber(evt.Damage);
                hpBar?.Hurt(evt.Damage);
            }

        }

        private void EmitDamageNumber(int dmg)
        {
            Debug.Log(dmg);
        }
    }
}