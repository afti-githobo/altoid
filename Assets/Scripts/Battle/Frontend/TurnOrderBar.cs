using Altoid.Battle.Types;
using Altoid.Battle.Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Frontend
{
    public class TurnOrderBar : MonoBehaviour
    {
        [SerializeField]
        private TurnOrderBarElement[] panels;
        // todo: if you can't adequately tell who's who from mugshots alone, should make these unique textures instead of colors
        [SerializeField]
        private Color playerColor;
        [SerializeField]
        private Color allyColor;
        [SerializeField]
        private Color enemyColor;
        [SerializeField]
        private Color thirdPartyColor;

        private Dictionary<BattlerIdentity, Sprite> mugshotTable = new();

        public void Load()
        {
            for (int i = 0; i < BattleRunner.Current.Battlers.Count; i++)
            {

            }
        }

        public void RepopulatePanels()
        {
            var turnOrder = new LinkedList<Battler>(BattleRunner.Current.TurnOrder);
            var node = turnOrder.First;
            for (int i = 0; i < panels.Length; i++)
            {
                if (!mugshotTable.ContainsKey(node.Value.BattlerDef.Identity))
                {
                    // load - should just do a separate load function really
                }
                if (node.Value.IsPlayerAlly()) panels[i].SetValues(playerColor, mugshotTable[node.Value.BattlerDef.Identity]);
                else if (node.Value.IsPlayerAlly()) panels[i].SetValues(allyColor, mugshotTable[node.Value.BattlerDef.Identity]);
                else if (node.Value.IsEnemy()) panels[i].SetValues(enemyColor, mugshotTable[node.Value.BattlerDef.Identity]);
                else panels[i].SetValues(thirdPartyColor, mugshotTable[node.Value.BattlerDef.Identity]);
                if (node.Next != null) node = node.Next;
                else node = turnOrder.First;
            }
        }
    }
}