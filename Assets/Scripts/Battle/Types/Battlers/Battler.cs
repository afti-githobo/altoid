using Altoid.Battle.Frontend;
using Altoid.Battle.Types.Action;
using Altoid.Battle.Types.Battlers.AI;
using Altoid.Battle.Types.Environment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Altoid.Battle.Types.Battlers
{
    public class Battler
    {
        public static event EventHandler<Battler> OnSpawn;
        public static event EventHandler<Battler> OnDamageTaken;
        public static event EventHandler<Battler> OnDamageHealed;
        public static event EventHandler<Battler> OnAttackEvaded;
        public static event EventHandler<Battler> OnDeath;
        public static event EventHandler<Battler> OnDelayed;
        public static event EventHandler<Battler> OnStatsRecalculated;
        public static event EventHandler<Battler> OnTurnStart;
        public static event EventHandler<Battler> OnTurnEnd;

        public readonly BattlerDef BattlerDef;
        public readonly BattlerInstanceDef InstanceDef;

        public IReadOnlyList<int> BaseStats { get; private set; }

        public List<StatusEffect> StatusEffects { get; private set; } = new();

        public readonly BattleRunner Parent;

        private ActionSource actionSource;

        public Battler(BattleRunner parent, BattlerInstanceDef instance)
        {
            BattlerDef = instance.Battler;
            InstanceDef = instance;
            BaseStats = BattlerDef.BaseStats;
            Parent = parent;
            Level = BattlerDef.BaseLevel + instance.LevelBonus;
            Stance = instance.StartingStance;
            IsDead = instance.Dead;
            IsHidden = instance.Hidden;
            CurrentHP = BattlerDef.BaseStats[Constants.STAT_MAX_HP] - instance.StartingDamage;
            Delay = instance.StartingDelay;
            actionSource = ActionSource.New(BattlerDef.ActionSource.Type, this);
            RecalculateStats();
        }

        public int Level { get; private set; }
        public int EntropyLevel { get; private set; }
        public StanceType Stance { get; private set; }
        public bool HasStances { get; private set; }
        /// <summary>
        /// This is *only* used by player battlers. AI-controlled battlers with stances just choose their actions in the AI.
        /// </summary>
        public IReadOnlyDictionary<StanceType, BattlerStance> PlayerStances { get; private set; }
        public IReadOnlyList<StanceType> PlayerStanceTypes { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsHidden { get; private set; }

        public IReadOnlyList<int> Stats { get => _stats; }
        private int[] _stats = { 0, 0, 0, 0, 0, 0};
        public int CurrentHP { get; private set; }
        public int Delay { get; private set; }

        public int ProvisionalDelay { get => Delay + _provisionalDelay; }
        private int _provisionalDelay;

        public async Task<BattleAction> GetAction() => await actionSource.SelectNextAction();

        private BattlerPuppet puppet;

        public void SupplyProvisionalTurnOrderData(int speedBonus, int speedMulti, int delay)
        {
            _provisionalDelay = Delay + delay;
        }

        public IReadOnlyList<TextAsset> GetScriptsToLoad() => actionSource.ActionLoadList;

        public void RecalculateStats()
        {
            // These loops could def. be compacted but we probably don't run this frequently enough for the optimization to matter
            RecalculateStat(BaseStats[Constants.STAT_MAX_HP], Constants.STAT_MAX_HP);
            RecalculateStat(BaseStats[Constants.STAT_ATK], Constants.STAT_ATK);
            RecalculateStat(BaseStats[Constants.STAT_DEF], Constants.STAT_DEF);
            RecalculateStat(BaseStats[Constants.STAT_DEX], Constants.STAT_DEX);
            RecalculateStat(BaseStats[Constants.STAT_AGI], Constants.STAT_AGI);
            RecalculateStat(BaseStats[Constants.STAT_SPD], Constants.STAT_SPD);
            OnStatsRecalculated?.Invoke(this, this);
        }

        public bool ExistsForTurnOrder => !IsDead && !IsHidden;

        private void RecalculateStat(int baseStat, int statIndex)
        {
            _stats[statIndex] = CalculateStat(baseStat, statIndex);
        }

        private int CalculateStat(int baseStat, int statIndex, int extraMod = 0, int extraMulti = 10000)
        {
            var stat = baseStat;
            for (int i = 0; i < StatusEffects.Count; i++)
            {
                stat *= StatusEffects[i].StatMultipliers[_stats[i]];
                stat /= 10000;
            }
            if (extraMulti != 10000)
            {
                stat *= extraMulti;
                stat /= 10000;
            }
            for (int i = 0; i < StatusEffects.Count; i++)
            {
                _stats[statIndex] += StatusEffects[i].StatModifiers[_stats[i]];
            }
            if (extraMod != 0) stat += extraMod;
            return stat;
        }

        public void DealDamage (int dmg)
        {      
            CurrentHP -= dmg;
            if (CurrentHP <= 0)
            {
                CurrentHP = 0;
                Die();
                puppet.StageDamageEvent(new BattlerPuppet.DamageEvent(dmg, false, true));
            }
            else
            {
                puppet.StageDamageEvent(new BattlerPuppet.DamageEvent(dmg, false, false));
            }
            OnDamageTaken?.Invoke(this, this);
        }

        public void MissWithAttack()
        {
            OnAttackEvaded?.Invoke(this, this);
        }

        public void Die()
        {
            IsDead = true;
            OnDeath?.Invoke(this, this);
        }

        public void ApplyDelay(int delay)
        {
            var spd = Stats[Constants.STAT_SPD] / Parent.SpeedFactor;
            Delay += delay * spd;
            OnDelayed?.Invoke(this, this);
        }

        public void RemoveDelay (int delay)
        {
            Delay -= delay;
        }

        public bool IsPlayer() => InstanceDef.Faction.HasFlag(BattleFaction.Player);

        public bool IsPlayerAlly() => InstanceDef.Faction.HasFlag(BattleFaction.GenericAlly);

        public bool IsThirdParty() => InstanceDef.Faction.HasFlag(BattleFaction.GenericThirdParty);

        public bool IsEnemy() => InstanceDef.Faction.HasFlag(BattleFaction.GenericEnemy);

    }
}