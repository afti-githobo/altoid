using Altoid.Battle.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Logic
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

        public event EventHandler<int> DoLoadBattleScene;
        public event EventHandler OnLoadedBattleScene;
        public event EventHandler OnLoadedPuppetBatch;
        public event EventHandler<string> OnSignal;

        public event EventHandler<string> OnScriptExecutionStarted;
        public event EventHandler<string> OnScriptExecutionEnded;
        public event EventHandler<string> OnScriptExecutionSuspended;
        public event EventHandler OnAllScriptExecutionEnded;

        public IReadOnlyList<Battler> Battlers { get => _battlers; }
        private List<Battler> _battlers;

        public BattleDef Definition { get; private set; }

        private BattleScript currentScript;
        private BattleScriptCmd currentCmd { get => (BattleScriptCmd)currentScript.Code[codePointer]; }

        private int currentScript_Next { get => currentScript.Code[codePointer++]; }

        private int codePointer;

        private Stack<Frame> _executionStack = new();

        private Dictionary<string, BattleScript> _scriptBank = new();

        private BattleScript GetScriptInBank(string id) => _scriptBank.ContainsKey(id) ? _scriptBank[id] : null;

        public async void Load(BattleDef battleDef)
        {
            Definition = battleDef;
            DoLoadBattleScene(this, Definition.InitialBattleScene.SceneIndex);
            // set up the battlers
            // stage the puppets
            // load the bgm
            //DoLoadPuppetBatch()
            
            // async: need to load battle scene and battler puppets...
        }

        public void LoadScripts(params TextAsset[] scripts)
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                var script = BattleScript.Parse(scripts[i]);
                _scriptBank[script.Name] = script;
            }
        }

        public void Step()
        {
            ExecuteBattleScriptCmd();
        }

        public bool IsExecutingScript { get => currentScript != null; }

        private void BeginExecutingScript(BattleScript script)
        {
            currentScript = script;
            codePointer = 0;
            OnScriptExecutionStarted?.Invoke(this, script.Name);
        }

        private void BranchToScript(BattleScript script)
        {
            _executionStack.Push(new Frame(currentScript, codePointer));
            BeginExecutingScript(script);
        }

        private void EndExecutingScript()
        {
            OnScriptExecutionEnded?.Invoke(this, currentScript.Name);
            if (_executionStack.Count > 0)
            {
                var frame = _executionStack.Pop();
                currentScript = frame.script;
                codePointer = frame.codePointer;
            } else
            {
                OnAllScriptExecutionEnded?.Invoke(this, EventArgs.Empty);
                currentScript = null;
                codePointer = 0;
                if (StackDepth > 0) Debug.LogWarning($"Script execution ended with a stack depth of {StackDepth}. This may indicate a problem with a script somewhere. If this is expected, ignore this message.");
                ClearStack();
            }
        }

        private void ExecuteBattleScriptCmd()
        {
            BattleScript.BattleScriptCmdTable[currentCmd].Invoke(this);
            codePointer++;
        }
    }
}