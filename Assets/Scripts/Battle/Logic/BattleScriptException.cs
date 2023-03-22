using System;

namespace Altoid.Battle.Logic
{
    public class BattleScriptException : Exception
    {
        public BattleScriptException(string msg) : base(msg) { }
    }
}