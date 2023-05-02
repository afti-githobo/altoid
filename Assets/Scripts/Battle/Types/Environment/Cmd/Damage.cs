namespace Altoid.Battle.Types.Environment
{
    public partial class BattleRunner
    {
        [BattleScript(BattleScriptCmd.CalcHit)]
        public void Cmd_CalcHit()
        {
            PopInt(out var baseAcc, out var hit, out var eva);
            var rnd = UnityEngine.Random.Range(0, 10001);
            var v = (((hit / eva) / 4) * 100 * baseAcc);
            if (v >= rnd) PushInt(1);
            else PushInt(0);
        }

        [BattleScript(BattleScriptCmd.CalcDamage)]
        public void Cmd_CalcDamage()
        {
            PopInt(out var baseDmg, out var atk, out var def);
            var rnd = UnityEngine.Random.Range(9250, 10750);
            PushInt((((atk / def) / 2) * 10000 * baseDmg) / rnd);
        }

        [BattleScript(BattleScriptCmd.DealDamage)]
        public void Cmd_DealDamage()
        {
            PopInt(out var dmg);
            Target.DealDamage(dmg);
        }

        [BattleScript(BattleScriptCmd.Miss)]
        public void Cmd_Miss()
        {
            Target.MissWithAttack();
        }
    }
}