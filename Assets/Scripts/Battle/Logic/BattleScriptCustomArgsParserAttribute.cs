using System;

namespace Altoid.Battle.Logic
{
    public class BattleScriptCustomArgsParserAttribute : Attribute
    {
        public readonly BattleScriptCmd Command;

        public BattleScriptCustomArgsParserAttribute(BattleScriptCmd command)
        {
            Command = command;
        }
    }
}