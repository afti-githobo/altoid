using System.Collections.Generic;

namespace Altoid.Battle.Types.Battlers
{
    public class StatusEffect
    {
        public readonly string Identifier;
        /// <summary>
        /// Multiplicative modifiers applied to stats by this status effect. These are expressed as percentages w/ two decimal points,
        /// out of 10000 - so, ex: 67.87 percent is 06787.
        /// </summary>
        public readonly IReadOnlyList<int> StatMultipliers;
        /// <summary>
        /// Additive (+/-) modifiers applied to stats by this status effect.
        /// </summary>
        public readonly IReadOnlyList<int> StatModifiers;
    }
}