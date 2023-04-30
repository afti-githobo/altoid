using System;

namespace Altoid.Battle.Types.Environment
{
    public class BattleScriptAttribute : Attribute
    {
        public readonly BattleScriptCmd ID;
        public readonly Type OperandType;

        public BattleScriptAttribute(BattleScriptCmd id, Type operandType = null)
        {
            ID = id;
            OperandType = operandType;
        }
    }
}