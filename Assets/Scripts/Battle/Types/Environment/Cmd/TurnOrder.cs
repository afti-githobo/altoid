using Altoid.Battle.Types.Battlers;
using System.Collections.Generic;

namespace Altoid.Battle.Types.Environment
{
    public partial class BattleRunner
    {
        public int SpeedFactor { get; private set; }
        public IReadOnlyList<Battler> TurnOrder { get => _turnOrder; }
        private IReadOnlyList<Battler> _turnOrder;

        private void RecalculateTurnOrder() => _turnOrder = CalculateTurnOrder(true);

        public IReadOnlyList<Battler> CalculateTurnOrder(bool makeStateChanges = true)
        {
            RecalculateSpeedFactor();
            var turnOrder = new List<Battler>(Battlers.Count);
            var battlersByDelay = new Dictionary<int, List<Battler>>();
            var delayValues = new List<int>();
            for (int i = 0; i < Battlers.Count; i++)
            {
                if (Battlers[i].ExistsForTurnOrder)
                {
                    var delay = Battlers[i].ProvisionalDelay;
                    if (!battlersByDelay.ContainsKey(delay)) battlersByDelay[delay] = new();
                    battlersByDelay[delay].Add(Battlers[i]);
                    if (!delayValues.Contains(delay)) delayValues.Add(delay);
                }
            }
            delayValues.Sort((v1, v2) => v1.CompareTo(v2));
            for (int i = 0; i < delayValues.Count; i++)
            {
                var battlers = battlersByDelay[delayValues[i]];
                while (battlers.Count > 0)
                {
                    turnOrder.Add(battlers[UnityEngine.Random.Range(0, battlers.Count)]);
                    battlers.Remove(turnOrder[turnOrder.Count - 1]);
                }
            }
            if (makeStateChanges)
            {
                for (int i = 0; i < Battlers.Count; i++)
                {
                    if (delayValues[0] > 0) Battlers[i].RemoveDelay(delayValues[0]);
                }
            }
            return turnOrder.ToArray();
        }

        private void RecalculateSpeedFactor ()
        {
            var q = 0;
            var v = 0;
            for (int i = 0; i < Battlers.Count; i++)
            {
                if (Battlers[i].ExistsForTurnOrder)
                {
                    q++;
                    v += Battlers[i].Stats[Constants.STAT_SPD];
                }
            }
            SpeedFactor = q / v;
        }
    }
}