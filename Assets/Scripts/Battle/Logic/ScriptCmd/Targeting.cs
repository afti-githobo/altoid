using System.Collections.Generic;

namespace Altoid.Battle.Logic
{
    public partial class BattleRunner
    {
        public Battler ActingBattler { get => _actingBattler; }
        private Battler _actingBattler;
        public IReadOnlyList<Battler> AllTargets { get => _allTargets; }
        private List<Battler> _allTargets;
        public IReadOnlyList<Battler> SelectedTarget { get => _selectedTargets; }
        private List<Battler> _selectedTargets;

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

        [BattleScript(BattleScriptCmd.AddActionTargets)]
        public void Cmd_AddActionTargets()
        {
            PopIntArray(out var battlerIndices);
            var battlers = new Battler[battlerIndices.Count];
            for (var i = 0; i < battlerIndices.Count; i++)
            {
                battlers[i] = Battlers[battlerIndices[i]];
            }
            AddActionTargets(battlers);
        }

        [BattleScript(BattleScriptCmd.ClearActionTargets)]
        public void Cmd_ClearActionTargets()
        {
            ClearActionTargets();
        }

        [BattleScript(BattleScriptCmd.ClearSelectedActionTargets)]
        public void Cmd_ClearSelectedActionTargets()
        {
            ClearSelectedActionTargets();
        }

        [BattleScript(BattleScriptCmd.RemoveActionTargets)]
        public void Cmd_RemoveActionTargets()
        {
            PopIntArray(out var battlerIndices);
            var battlers = new Battler[battlerIndices.Count];
            for (var i = 0; i < battlerIndices.Count; i++)
            {
                battlers[i] = Battlers[battlerIndices[i]];
            }
            RemoveActionTargets(battlers);
        }

        [BattleScript(BattleScriptCmd.SelectActionTargets)]
        public void Cmd_SelectActionTargets()
        {
            PopIntArray(out var battlerIndices);
            var battlers = new Battler[battlerIndices.Count];
            for (var i = 0; i < battlerIndices.Count; i++)
            {
                battlers[i] = Battlers[battlerIndices[i]];
            }
            SelectActionTargets(battlers);
        }

        [BattleScript(BattleScriptCmd.SetActingBattler)]
        public void Cmd_SetActingBattler()
        {
            PopInt(out var battlerIndex);
            SetActingBattler(Battlers[battlerIndex]);
        }

        [BattleScript(BattleScriptCmd.UnselectActionTargets)]
        public void Cmd_UnselectActionTargets()
        {
            PopIntArray(out var battlerIndices);
            var battlers = new Battler[battlerIndices.Count];
            for (var i = 0; i < battlerIndices.Count; i++)
            {
                battlers[i] = Battlers[battlerIndices[i]];
            }
            UnselectActionTargets(battlers);
        }

        [BattleScript(BattleScriptCmd.UnsetActingBattler)]
        public void Cmd_UnsetActingBattler()
        {
            UnsetActingBattler();
        }
    }
}