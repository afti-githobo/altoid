using Altoid.Battle.Types.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Altoid.Battle.Types.Battlers.AI
{
    class ActionSource_Dummy : ActionSource
    {
        public ActionSource_Dummy(Battler b) : base(b) { }

        public override Task<BattleAction> SelectNextAction()
        {
            
            throw new NotImplementedException();
        }
    }
}
