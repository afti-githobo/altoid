using System;
using UnityEngine;

namespace Altoid.Battle.Logic
{
   public abstract class BattleEventAction
    {
        public abstract void Run(BattleRunner manager);
    }
}