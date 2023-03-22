using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altoid.Battle.Logic;

namespace Altoid.Battle.AI
{
    public abstract class BattlerAIScript
    {
        protected readonly Battler battler;
        protected readonly BattleRunner runner;
        LinkedList<EventHandler<string>> signalHooks = new();

        public BattlerAIScript(Battler _battler, BattleRunner _runner)
        {
            battler = _battler;
            runner = _runner;
            Init();
        }

        public abstract void Init();

        public void Cleanup()
        {
            while (signalHooks.Count > 0)
            {
                runner.OnSignal -= signalHooks.Last.Value;
                signalHooks.RemoveLast();
            }
        }

        protected void HookSignal<T>(string signalName, Action action)
        {
            EventHandler<string> func = (object sender, string data) =>
            {
                if (data == signalName) action();
            };
            signalHooks.AddLast(func);
            runner.OnSignal += func;
        }

        public abstract Task<BattleAction> NextAction();
    }
}