using Altoid.Battle.Data;
using System;
using System.Collections.Generic;

namespace Altoid.Battle.Logic
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

        public BattleStatBlock BaseStats { get; private set; }

        public List<StatusEffect> StatusEffects { get; private set; } = new();

        public readonly BattleRunner Parent;

        public int Level { get; private set; }
        public int EntropyLevel { get; private set; }
        public int Stance { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsHidden { get; private set; }

        public IReadOnlyList<int> Stats { get => _stats; }
        private int[] _stats = { 0, 0, 0, 0, 0, 0};
        public int CurrentHP { get; private set; }
        public int Delay { get; private set; }

        public int ProvisionalDelay { get => Delay + _provisionalDelay; }
        private int _provisionalDelay;
        private int _provisionalSpeed;

        public void SupplyProvisionalTurnOrderData(int speedBonus, int speedMulti, int delay)
        {
            _provisionalDelay = Delay + delay;

        }

        public void RecalculateStats()
        {
            // These loops could def. be compacted but we probably don't run this frequently enough for the optimization to matter
            RecalculateStat(BaseStats.MaxHP, Constants.STAT_MAX_HP);
            RecalculateStat(BaseStats.Attack, Constants.STAT_ATK);
            RecalculateStat(BaseStats.Defense, Constants.STAT_DEF);
            RecalculateStat(BaseStats.Dexterity, Constants.STAT_DEX);
            RecalculateStat(BaseStats.Agility, Constants.STAT_AGI);
            RecalculateStat(BaseStats.Speed, Constants.STAT_SPD);
            OnStatsRecalculated?.Invoke(this, this);
        }

        public bool ShouldBeCountedForSpeedFactorCalculations => !IsDead && !IsHidden;

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
    }
}