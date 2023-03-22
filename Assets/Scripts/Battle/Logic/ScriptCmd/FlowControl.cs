namespace Altoid.Battle.Logic
{
    public partial class BattleRunner
    {
        private void JumpToLabel(string label)
        {
            if (!currentScript.Labels.ContainsKey(label)) throw new BattleScriptException($"{currentCmd} attempted jump to nonexistant label ${label}");
            codePointer = currentScript.Labels[label];
        }

        [BattleScript(BattleScriptCmd.JumpUnconditional, typeof(string))]
        public void Cmd_JumpUnconditional()
        {
            PopString(out string label);
            JumpToLabel(label);
        }

        [BattleScript(BattleScriptCmd.JumpConditional, typeof(string))]
        public void Cmd_JumpConditional()
        {
            if (GetComparisonVal())
            {
                PopString(out string label);
                JumpToLabel(label);
            }
        }
    }
}