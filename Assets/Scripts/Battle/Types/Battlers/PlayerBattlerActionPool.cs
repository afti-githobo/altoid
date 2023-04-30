using Altoid.Battle.Types.Action;
using System.Collections.Generic;

namespace Altoid.Battle.Types.Battlers
{
    public class PlayerBattlerActionPool
    {
        public IReadOnlyList<StanceType> Stances { get => _stances; }
        private StanceType[] _stances;
        public IReadOnlyDictionary<StanceType, IReadOnlyList<BattleAction>> StandardActionsByStance { get => _standardActionsByStance; }
        private Dictionary<StanceType, IReadOnlyList<BattleAction>> _standardActionsByStance;
        public IReadOnlyDictionary<StanceType, BattleAction> StanceEntryActions { get => _stanceEntryActions; }
        private Dictionary<StanceType, BattleAction> _stanceEntryActions;

        public IReadOnlyDictionary<StanceType, BattleAction> StanceUltimates { get => _stanceUltimates; }
        private Dictionary<StanceType, BattleAction> _stanceUltimates;
    }
}