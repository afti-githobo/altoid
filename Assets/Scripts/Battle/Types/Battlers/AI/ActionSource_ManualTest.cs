using Altoid.Battle.Types.Action;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Altoid.Battle.Types.Battlers.AI
{
    public class ActionSource_ManualTest : ActionSource
    {
        private static Task<BattleAction> generatedTask;
        private static BattleAction selectedAction;

        public static void SubmitAction(BattleAction action)
        {
            if (generatedTask == null) throw new Exception("Can't submit an action until SelectNextAction is called");
            selectedAction = action;
            generatedTask.Start();
        }

        public ActionSource_ManualTest(Battler b) : base(b) { }

        public override IReadOnlyList<TextAsset> ScriptLoadList => new TextAsset[] { new TextAsset(), new TextAsset(), new TextAsset() };

        public override Task<BattleAction> SelectNextAction()
        {
            return generatedTask = new Task<BattleAction>( () => {
                generatedTask = null;
                return selectedAction; 
            } );
        }
    }
}