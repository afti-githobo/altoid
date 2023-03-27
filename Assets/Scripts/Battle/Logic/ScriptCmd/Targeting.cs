using System;
using System.Collections.Generic;

namespace Altoid.Battle.Logic
{
    public partial class BattleRunner
    {
        public Battler ActingBattler { get => _actingBattler; }
        private Battler _actingBattler;
        public IReadOnlyList<Battler> TargetGroup0 { get => _targetGroup0; }
        private List<Battler> _targetGroup0 = new();
        public IReadOnlyList<Battler> TargetGroup1 { get => _targetGroup1; }
        private List<Battler> _targetGroup1 = new();
        public IReadOnlyList<Battler> TargetGroup2 { get => _targetGroup2; }
        private List<Battler> _targetGroup2 = new();
        public IReadOnlyList<Battler> TargetGroup3 { get => _targetGroup3; }
        private List<Battler> _targetGroup3 = new();
        public IReadOnlyList<Battler> TargetGroup4 { get => _targetGroup4; }
        private List<Battler> _targetGroup4 = new();
        public IReadOnlyList<Battler> TargetGroup5 { get => _targetGroup5; }
        private List<Battler> _targetGroup5 = new();
        public IReadOnlyList<Battler> TargetGroup6 { get => _targetGroup6; }
        private List<Battler> _targetGroup6 = new();
        public IReadOnlyList<Battler> TargetGroup7 { get => _targetGroup7; }
        private List<Battler> _targetGroup7 = new();

        private List<Battler>[] TargetGroups { get => _targetGroups ?? GetTargetGroups(); }
        private List<Battler>[] _targetGroups;

        private List<Battler>[] GetTargetGroups() => _targetGroups = new List<Battler>[] { _targetGroup0, _targetGroup1, _targetGroup2, _targetGroup3, _targetGroup4, _targetGroup5, _targetGroup6, _targetGroup7 };

        private void SetActingBattler(Battler b)
        {
            _actingBattler = b;
        }

        private void UnsetActingBattler() => _actingBattler = null;

        [BattleScript(BattleScriptCmd.SetActingBattler)]
        public void Cmd_SetActingBattler()
        {
            PopInt(out var battlerIndex);
            SetActingBattler(Battlers[battlerIndex]);
        }

        [BattleScript(BattleScriptCmd.UnsetActingBattler)]
        public void Cmd_UnsetActingBattler()
        {
            UnsetActingBattler();
        }

        [BattleScript(BattleScriptCmd.AddTargetsToGroup, typeof(int))]
        public void Cmd_AddTargetsToGroup()
        {
            PopInt(out var grp);
            PopIntArray(out var battlerIds);
            if (grp < 0 || grp > 8) throw new BattleScriptException($"{grp} is not a valid target group");
            var group = TargetGroups[grp];
            for (int i = 0; i < battlerIds.Count; i++)
            {
                var battler = Battlers[battlerIds[i]];
                if (!group.Contains(battler)) group.Add(battler);
            }
        }

        [BattleScript(BattleScriptCmd.RemoveTargetsFromGroup, typeof(int))]
        public void Cmd_RemoveTargetsFromGroup()
        {
            PopInt(out var grp);
            PopIntArray(out var battlerIds);
            if (grp < 0 || grp > 8) throw new BattleScriptException($"{grp} is not a valid target group");
            var group = TargetGroups[grp];
            for (int i = 0; i < battlerIds.Count; i++)
            {
                var battler = Battlers[battlerIds[i]];
                group.Remove(battler);
            }
        }

        [BattleScript(BattleScriptCmd.ClearGroup, typeof(int))]
        public void Cmd_ClearGroup()
        {
            PopInt(out var grp);
            ClearGroup(grp);
            if (grp < 0 || grp > 8) throw new BattleScriptException($"{grp} is not a valid target group");
            var group = TargetGroups[grp];
            group.Clear();
        }

        [BattleScript(BattleScriptCmd.ClearAllGroups)]
        public void Cmd_ClearAllGroups()
        {
            ClearAllGroups();
        }

        public void ClearAllGroups ()
        {
            for (int i = 0; i < TargetGroups.Length; i++) TargetGroups[i].Clear();
        }

        public void ClearGroup(int grp)
        {
            // this seems like it should be called by cmd_cleargroup, but that'd require some refactoring that'd be more trouble than it's worth.
            // methods should not throw BattleScriptException unless they're being executed by the script interpreter.
            if (grp < 0 || grp > 8) throw new Exception($"{grp} is not a valid target group");
            var group = TargetGroups[grp];
            group.Clear();
        }

        public void AddTargetsToGroup(int grp, params Battler[] battlers)
        {
            if (grp < 0 || grp > 8) throw new Exception($"{grp} is not a valid target group");
            var group = TargetGroups[grp];
            for (int i = 0; i < battlers.Length; i++)
            {
                var battler = battlers[i];
                if (!group.Contains(battler)) group.Add(battler);
            }
        }

        public void RemoveTargetsFromGroup(int grp, params Battler[] battlers)
        {
            if (grp < 0 || grp > 8) throw new Exception($"{grp} is not a valid target group");
            var group = TargetGroups[grp];
            for (int i = 0; i < battlers.Length; i++)
            {
                var battler = battlers[i];
                group.Remove(battler);
            }
        }
    }
}