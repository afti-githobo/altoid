using Altoid.Battle.Data.Action;

namespace Altoid.Battle.Logic
{
    public class BattleAction
    {
        public readonly BattleActionDef Record;

        public BattleAction(BattleActionDef record)
        {
            Record = record;
        }
    }
}