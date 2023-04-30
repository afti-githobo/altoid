namespace Altoid.Battle.Types.Action
{
    public class BattleAction
    {
        public readonly string Name;
        public readonly string Description;
        public readonly BattleActionDef Record;

        public BattleAction(BattleActionDef record)
        {
            Record = record;
        }
    }
}