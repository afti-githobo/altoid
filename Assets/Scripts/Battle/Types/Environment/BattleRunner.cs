using Altoid.Battle.Types.Action;
using Altoid.Battle.Types.Battle;
using Altoid.Battle.Types.Battlers;
using System;
using System.Collections.Generic;

using UnityEngine;

namespace Altoid.Battle.Types.Environment
{
    public partial class BattleRunner
    {
        public static BattleRunner Current { get; private set; }

        public BattleRunner(BattleDef d)
        {
            if (d == null)
            {
                Debug.LogWarning("Is a test running? BattleRunner instantiated without BattleDef");
                return;
            }
            Current = this;
            Load(d);
            InitializeBattlers();
        }

        public event EventHandler<int> DoLoadBattleScene;
        public event EventHandler OnLoadedBattleScene;
        public event EventHandler OnLoadedPuppetBatch;
        

        public event EventHandler<string> OnScriptExecutionStarted;
        public event EventHandler<string> OnScriptExecutionEnded;
        public event EventHandler<string> OnScriptExecutionSuspended;
        public event EventHandler OnAllScriptExecutionEnded;

        public IReadOnlyList<Battler> Battlers { get => _battlers; }
        private List<Battler> _battlers;

        public BattleDef Definition { get; private set; }

        public int Delay { get; private set; }

        private BattleAction currentAction;
        private int currentPacketIndex;
        private BattleScript currentScript;
        private BattleActionDef.Packet currentPacket;
        private BattleScriptCmd currentCmd;

        private int currentScript_Next { get => currentScript?.Code[codePointer+=1] ?? (int)BattleScriptCmd.Invalid; }

        private int codePointer;

        private Dictionary<string, BattleScript> _scriptBank = new();

        private BattleScript GetScriptInBank(string id) => _scriptBank.ContainsKey(id) ? _scriptBank[id] : null;

        private Queue<string> scriptQueue = new();

        public bool ReadyToRunBattleLogic => currentAction != null;

        private void InitializeBattlers()
        {
            for (int i = 0; i < Definition.Battlers.Count; i++)
            {
                _battlers.Add(new Battler(this, Definition.Battlers[i]));
                LoadScripts(_battlers[i].GetScriptsToLoad());
            }
            CalculateTurnOrder();
        }

        public void RunBattleLogic()
        {
            if (currentAction == null) throw new Exception("Cannot run battle logic while awaiting an action");
            if (currentPacket == null) StepPackets();
            while (!scriptExecutionSuspended && currentScript != null)
            {
                while (!scriptExecutionSuspended && currentScript != null) StepScripts();
                if (currentScript == null && scriptQueue.Count > 0) RunScript(scriptQueue.Dequeue());
            }
           
        }

        public void Load(BattleDef battleDef)
        {
            Definition = battleDef;
            LoadScripts(Resources.Load<TextAsset>(Constants.END_ACTION_PACKET_SCRIPT_FILENAME));
            for (int i = 0; i < battleDef.Battlers.Count; i++)
            {
                var instance = battleDef.Battlers[i];
                var battler = new Battler(this, instance);
                _battlers.Add(battler);
            }
        }

        public async void GetNextAction()
        {
            SetActingBattler(TurnOrder[0]);
            currentAction = await ActingBattler.GetAction();
            currentPacketIndex = 0;
        }

        public void LoadScripts(params BattleScript[] scripts)
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                _scriptBank[scripts[i].Name] = scripts[i];
            }
        }

        public void LoadScripts(params TextAsset[] scripts)
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                var script = BattleScript.Parse(scripts[i]);
                _scriptBank[script.Name] = script;
            }
        }

        public void LoadScripts(IReadOnlyList<TextAsset> scripts)
        {
            for (int i = 0; i < scripts.Count; i++)
            {
                var script = BattleScript.Parse(scripts[i]);
                _scriptBank[script.Name] = script;
            }
        }

        private void StepPackets()
        {
            if (currentPacketIndex == currentAction.Record.Packets.Count) currentAction = null; // action execution has finished
            else
            {
                currentPacket = currentAction.Record.Packets[currentPacketIndex];
                DispatchScriptsForActionPacket();
            }
        }

        private void DispatchScriptsForActionPacket()
        {
            var packet = currentAction.Record.Packets[currentPacketIndex];
            for (int i = 0; i < packet.ParamsFloat.Count; i++) PushFloat(packet.ParamsFloat[i]);
            for (int i = 0; i < packet.ParamsInt.Count; i++) PushInt(packet.ParamsInt[i]);
            for (int i = 0; i < packet.ParamsString.Count; i++) PushString(packet.ParamsString[i]);
            for (int i = 0; i < packet.Scripts.Count; i++) scriptQueue.Enqueue(packet.Scripts[i].name);
            scriptQueue.Enqueue(Constants.END_ACTION_PACKET_SCRIPT);
        }

        public void StepScripts()
        {
            if (currentScript != null)
            {
                ExecuteBattleScriptCmd();
                if (codePointer == currentScript.Code.Count) EndExecutingScript();
            }

        }

        public void RunScript(string scriptName)
        {
            var script = GetScriptInBank(scriptName);
            if (script == null) throw new BattleScriptException($"No script {scriptName} present in bank - cannot begin execution");
            BeginExecutingScript(script);
        }

        public bool IsExecutingScript { get => currentScript != null; }

        private void BeginExecutingScript(BattleScript script)
        {
            if (currentScript != null) codePointer = -1; // Offset starting code pointer if already executing - it's always incremented as the last step of executing a command
            else codePointer = 0;
            currentScript = script;
            OnScriptExecutionStarted?.Invoke(this, script.Name);
        }

        private void EndExecutingScript()
        {
            OnScriptExecutionEnded?.Invoke(this, currentScript.Name);
            if (executionStack.Count > 0) Debug.LogWarning($"Script execution ended with a call stack depth of {executionStack.Count}. This may indicate a problem with a script somewhere. If this is expected, ignore this message.");
            OnAllScriptExecutionEnded?.Invoke(this, EventArgs.Empty);
            currentScript = null;
            codePointer = 0;
            if (StackDepth > 0) Debug.LogWarning($"Script execution ended with a stack depth of {StackDepth}. This may indicate a problem with a script somewhere. If this is expected, ignore this message.");
            ClearStack();
        }

        private void ExecuteBattleScriptCmd()
        {
            currentCmd = (BattleScriptCmd)currentScript.Code[codePointer];
            BattleScript.BattleScriptCmdTable[currentCmd].Invoke(this);
            codePointer++;
        }
    }
}