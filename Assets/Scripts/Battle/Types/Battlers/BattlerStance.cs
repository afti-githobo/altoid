using Altoid.Battle.Types.Action;
using System.Collections.Generic;

namespace Altoid.Battle.Types.Battlers
{
    public class BattlerStance
    {
        public IReadOnlyList<BattleAction> StandardActions { get => _standardActions; }
        private BattleAction[] _standardActions;
        public BattleAction StanceEntryAction { get => _stanceEntryAction; }
        private BattleAction _stanceEntryAction;

        public BattleAction StanceUltimate { get => _stanceUltimate; }
        private BattleAction _stanceUltimate;
    }
}