using Altoid.Battle.Data;
using System;

namespace Altoid.Battle.Logic
{
    public class SignalHandle
    {
        public bool Released { get; private set; }
        public readonly BattleSignal Signal;
        public readonly BattleRunner BattleRunner;

        public SignalHandle(BattleSignal signal, BattleRunner battleRunner)
        {
            Signal = signal;
            BattleRunner = battleRunner;
        }

        public void Release()
        {
            Released = true;
            BattleRunner.ReleaseSignalHandle(this);
        }
    }
}