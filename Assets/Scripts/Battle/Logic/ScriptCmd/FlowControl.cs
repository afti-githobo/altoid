namespace Altoid.Battle.Logic
{
    public partial class BattleRunner
    {
        private void JumpToLabel(string label)
        {
            if (!currentScript.Labels.ContainsKey(label)) throw new BattleScriptException($"{currentCmd} attempted jump to nonexistant label ${label}");
            codePointer = currentScript.Labels[label];
        }

        [BattleScript(BattleScriptCmd.Nop)]
        public void Cmd_Nop() => UnityEngine.Debug.Log($"{currentScript.Name} - nop");

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

        [BattleScript(BattleScriptCmd.JumpToScriptUnconditional, typeof(string))]
        public void Cmd_JumpToScriptUnconditional()
        {
            PopString(out string id);
            var script = GetScriptInBank(id);
            if (script == null) throw new BattleScriptException($"Attempted jump to script {id} not present in bank");
            BeginExecutingScript(script);
        }

        [BattleScript(BattleScriptCmd.JumpToScriptConditional, typeof(string))]
        public void Cmd_JumpToScriptConditional()
        {
            if (GetComparisonVal())
            {
                PopString(out string id);
                var script = GetScriptInBank(id);
                if (script == null) throw new BattleScriptException($"Attempted jump to script {id} not present in bank");
                BeginExecutingScript(script);
            }
        }

        [BattleScript(BattleScriptCmd.BranchToScriptUnconditional, typeof(string))]
        public void Cmd_BranchToScriptUnconditional()
        {
            PopString(out string id);
            var script = GetScriptInBank(id);
            if (script == null) throw new BattleScriptException($"Attempted branch to script {id} not present in bank");
            BranchToScript(script);
        }

        [BattleScript(BattleScriptCmd.BranchToScriptConditional, typeof(string))]
        public void Cmd_BranchToScriptConditional()
        {
            if (GetComparisonVal())
            {
                PopString(out string id);
                var script = GetScriptInBank(id);
                if (script == null) throw new BattleScriptException($"Attempted branch to script {id} not present in bank");
                BranchToScript(script);
            }
        }
    }
}