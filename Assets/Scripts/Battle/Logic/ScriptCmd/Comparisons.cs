namespace Altoid.Battle.Logic
{
    public partial class BattleRunner
    {
        private bool GetComparisonVal() => _PopInt() > 0;

        [BattleScript(BattleScriptCmd.FloatEquals, typeof(float))]
        public void Cmd_FloatEquals()
        {
            PopFloat(out var a, out var b);
            if (a == b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.FloatNotEquals, typeof(float))]
        public void Cmd_FloatNotEquals()
        {
            PopFloat(out var a, out var b);
            if (a != b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.FloatGreaterThan, typeof(float))]
        public void Cmd_FloatGreaterThan()
        {
            PopFloat(out var a, out var b);
            if (a > b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.FloatGreaterThanEquals, typeof(float))]
        public void Cmd_FloatGreaterThanEquals()
        {
            PopFloat(out var a, out var b);
            if (a >= b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.FloatLessThan, typeof(float))]
        public void Cmd_FloatLessThan()
        {
            PopFloat(out var a, out var b);
            if (a < b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.FloatLessThanEquals, typeof(float))]
        public void Cmd_FloatLessThanEquals()
        {
            PopFloat(out var a, out var b);
            if (a <= b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.IntEquals, typeof(int))]
        public void Cmd_IntEquals()
        {
            PopInt(out var a, out var b);
            if (a == b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.IntNotEquals, typeof(int))]
        public void Cmd_IntNotEquals()
        {
            PopInt(out var a, out var b);
            if (a != b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.IntGreaterThan, typeof(int))]
        public void Cmd_IntGeaterThan()
        {
            PopInt(out var a, out var b);
            if (a > b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.IntGreaterThanEquals, typeof(int))]
        public void Cmd_IntGreaterThanEquals()
        {
            PopInt(out var a, out var b);
            if (a >= b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.IntLessThan, typeof(int))]
        public void Cmd_IntLessThan()
        {
            PopInt(out var a, out var b);
            if (a < b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.IntLessThanEquals, typeof(int))]
        public void Cmd_IntLessThanEquals()
        {
            PopInt(out var a, out var b);
            if (a <= b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.StringEquals, typeof(string))]
        public void Cmd_StringEquals()
        {
            PopString(out var a, out var b);
            if (a == b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.StringNotEquals, typeof(string))]
        public void Cmd_StringNotEquals()
        {
            PopString(out var a, out var b);
            if (a != b) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.StringContains, typeof(string))]
        public void Cmd_StringContains()
        {
            PopString(out var a, out var b);
            if (a.Contains(b)) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }

        [BattleScript(BattleScriptCmd.StringNotContains, typeof(string))]
        public void Cmd_StringDoesNotContain()
        {
            PopString(out var a, out var b);
            if (!a.Contains(b)) PushInt(Constants.TRUE);
            else PushInt(Constants.FALSE);
        }
    }
}