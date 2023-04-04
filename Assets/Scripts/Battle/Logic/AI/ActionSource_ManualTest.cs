using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Altoid.Battle.Logic.AI
{
    public class ActionSource_ManualTest : ActionSource
    {
        public ActionSource_ManualTest(Battler b) : base(b) { }

        public override IReadOnlyList<TextAsset> ActionLoadList => throw new System.NotImplementedException();

        public override async Task<BattleAction> SelectNextAction()
        {
            throw new System.NotImplementedException();
        }
    }
}