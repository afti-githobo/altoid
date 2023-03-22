namespace Altoid.Battle.Logic
{
    public partial class BattleRunner
    {
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