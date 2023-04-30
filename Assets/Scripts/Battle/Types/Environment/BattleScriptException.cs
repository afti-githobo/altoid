using System;

namespace Altoid.Battle.Types.Environment
{
    public class BattleScriptException : Exception
    {
        public BattleScriptException(string msg) : base(msg) { }
    }
}