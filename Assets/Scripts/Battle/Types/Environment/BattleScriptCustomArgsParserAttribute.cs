using System;

namespace Altoid.Battle.Types.Environment
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