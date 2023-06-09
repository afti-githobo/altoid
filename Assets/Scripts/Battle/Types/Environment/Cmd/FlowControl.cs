﻿using System.Collections.Generic;

namespace Altoid.Battle.Types.Environment
{
    public partial class BattleRunner
    {
        private readonly struct Frame
        {
            public readonly BattleScript script;
            public readonly int codePointer;

            public Frame(BattleScript script, int codePointer)
            {
                this.script = script;
                this.codePointer = codePointer;
            }
        }

        private Stack<Frame> executionStack = new();

        private bool ReturnFromBranch()
        {
            if (executionStack.Count > 0)
            {
                var frame = executionStack.Pop();
                currentScript = frame.script;
                codePointer = frame.codePointer;
                return true;
            }
            return false;
        }

        private void JumpToLabel(string label)
        {
            if (!currentScript.Labels.ContainsKey(label)) throw new BattleScriptException($"{currentCmd} attempted jump to nonexistant label ${label}");
            codePointer = currentScript.Labels[label] - 1; // Code pointer advances after this runs, meaning we want to set it to v - 1 so we increment to the correct position
        }

        private void JumpToScript(string id)
        {
            var script = GetScriptInBank(id);
            if (script == null) throw new BattleScriptException($"Attempted jump to script {id} not present in bank");
            BeginExecutingScript(script);
        }

        private void BranchToLabel(string label)
        {
            if (!currentScript.Labels.ContainsKey(label)) throw new BattleScriptException($"{currentCmd} attempted jump to nonexistant label ${label}");
            executionStack.Push(new Frame(currentScript, codePointer));
            codePointer = currentScript.Labels[label] - 1; // Code pointer advances after this runs, meaning we want to set it to v - 1 so we increment to the correct position
        }

        private void BranchToScript(BattleScript script)
        {
            executionStack.Push(new Frame(currentScript, codePointer));
            BeginExecutingScript(script);
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
            PopString(out string label);
            if (GetComparisonVal()) JumpToLabel(label);
        }

        [BattleScript(BattleScriptCmd.JumpToScriptUnconditional, typeof(string))]
        public void Cmd_JumpToScriptUnconditional()
        {
            PopString(out string id);
            JumpToScript(id);
        }

        [BattleScript(BattleScriptCmd.JumpToScriptConditional, typeof(string))]
        public void Cmd_JumpToScriptConditional()
        {
            PopString(out string id);
            if (GetComparisonVal()) JumpToScript(id);
        }

        [BattleScript(BattleScriptCmd.BranchUnconditional, typeof(string))]
        public void Cmd_BranchUnconditional()
        {
            PopString(out string label);
            BranchToLabel(label);
        }

        [BattleScript(BattleScriptCmd.BranchConditional, typeof(string))]
        public void Cmd_BranchConditional()
        {
            PopString(out string label);
            if (GetComparisonVal()) BranchToLabel(label);
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
            PopString(out string id);
            if (GetComparisonVal())
            {
                var script = GetScriptInBank(id);
                if (script == null) throw new BattleScriptException($"Attempted branch to script {id} not present in bank");
                BranchToScript(script);
            }
        }

        [BattleScript(BattleScriptCmd.Return)]
        public void Cmd_Return()
        {
            ReturnFromBranch();
        }
    }
}