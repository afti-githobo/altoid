namespace Altoid.Battle.Types.Environment
{
    public partial class BattleRunner
    {
        [BattleScript(BattleScriptCmd.GetActingBattlerStat, typeof(int))]
        public void Cmd_GetActingBattlerStat ()
        {
            PopInt(out var stat);
            if (stat < Constants.STAT_MAX_HP || stat > Constants.STAT_SPD) throw new BattleScriptException($"{stat} is not a valid argument for GetActingBattlerStat");
            PushInt(ActingBattler.Stats[stat]);
        }

        [BattleScript(BattleScriptCmd.GetTargetStat, typeof(int))]
        public void Cmd_GetTargetStat()
        {
            PopInt(out var stat);
            if (stat < Constants.STAT_MAX_HP || stat > Constants.STAT_SPD) throw new BattleScriptException($"{stat} is not a valid argument for GetTargetStat");
            PushInt(Target.Stats[stat]);
        }

        [BattleScript(BattleScriptCmd.GetActingBattlerLevel)]
        public void Cmd_GetActingBattlerLevel()
        {
            PushInt(ActingBattler.Level);
        }

        [BattleScript(BattleScriptCmd.GetTargetLevel, typeof(int))]
        public void Cmd_GetTargetLevel()
        {
            PushInt(Target.Level);
        }
    }
}