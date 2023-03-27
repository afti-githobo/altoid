using Altoid.Battle.Data;
using System;
using System.Collections.Generic;

namespace Altoid.Battle.Logic
{
    public class Battler
    {
        public BattleStatBlock BaseStats { get; private set; }
        public int Level { get; private set; }

        public IReadOnlyList<int> Stats { get => _stats; }
        private int[] _stats;

        public void DealDamage (int dmg)
        {
            throw new NotImplementedException();
        }

        public void MissWithAttack()
        {
            throw new NotImplementedException();
        }
    }
}