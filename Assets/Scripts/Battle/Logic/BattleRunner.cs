using Altoid.Battle.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altoid.Battle.Logic
{
    public partial class BattleRunner
    {
        public event EventHandler<int> DoLoadBattleScene;
        public event EventHandler OnLoadedBattleScene;
        public event EventHandler OnLoadedPuppetBatch;
        public event EventHandler<string> OnSignal;

        public IReadOnlyList<Battler> Battlers { get => _battlers; }
        private List<Battler> _battlers;

        public BattleDef Definition { get; private set; }

        public Battler ActingBattler { get => _actingBattler; }
        private Battler _actingBattler;
        public IReadOnlyList<Battler> AllTargets { get => _allTargets; }
        private List<Battler> _allTargets;
        public IReadOnlyList<Battler> SelectedTarget { get => _selectedTargets; }
        private List<Battler> _selectedTargets;

        private BattleScript currentScript;
        private BattleScriptCmd currentCmd { get => (BattleScriptCmd)currentScript.Code[codePointer]; }

        private int currentScript_Next { get => currentScript.Code[codePointer++]; }

        private int codePointer;

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

        private void AddActionTargets(Battler[] battlers)
        {
            for (int i = 0; i < battlers.Length; i++)
            {
                _allTargets.Add(battlers[i]);
            }
        }

        private void ClearActionTargets()
        {
            _allTargets.Clear();
            _selectedTargets.Clear();
        }
        private void ClearSelectedActionTargets()
        {
            _selectedTargets.Clear();
        }

        private void RemoveActionTargets(Battler[] battlers)
        {
            for (int i = 0; i < battlers.Length; i++)
            {
                _allTargets.Remove(battlers[i]);
            }
        }

        private void SetActingBattler(Battler b)
        {
            _actingBattler = b;
        }

        private void SelectActionTargets(Battler[] battlers)
        {
            for (int i = 0; i < battlers.Length; i++)
            {
                _selectedTargets.Add(battlers[i]);
            }
        }

        private void UnselectActionTargets(Battler[] battlers)
        {
            for (int i = 0; i < battlers.Length; i++)
            {
                _selectedTargets.Remove(battlers[i]);
            }
        }

        private void UnsetActingBattler() => _actingBattler = null;

        private void ExecuteBattleScriptCmd() => BattleScript.BattleScriptCmdTable[currentCmd].Invoke(this);
    }
}