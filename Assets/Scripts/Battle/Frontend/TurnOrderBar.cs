using Altoid.Battle.Types.Battlers;
using Altoid.Battle.Types.Environment;
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

        private Dictionary<BattlerAssetType, Sprite> mugshotTable = new();

        public void Load()
        {
            for (int i = 0; i < BattleRunner.Current.Battlers.Count; i++)
            {
                if (!mugshotTable.ContainsKey(BattleRunner.Current.Battlers[i].BattlerDef.AssetType)) 
                    Resources.Load<Sprite>($"{Constants.RESOURCE_PATH_BATTLER_MUGSHOTS}/{BattleRunner.Current.Battlers[i].BattlerDef.AssetType}");
            }
        }

        public void RepopulatePanels()
        {
            Load();
            var turnOrder = new LinkedList<Battler>(BattleRunner.Current.TurnOrder);
            var node = turnOrder.First;
            for (int i = 0; i < panels.Length; i++)
            {
                if (node.Value.IsPlayerAlly()) panels[i].SetValues(playerColor, mugshotTable[node.Value.BattlerDef.AssetType]);
                else if (node.Value.IsPlayerAlly()) panels[i].SetValues(allyColor, mugshotTable[node.Value.BattlerDef.AssetType]);
                else if (node.Value.IsEnemy()) panels[i].SetValues(enemyColor, mugshotTable[node.Value.BattlerDef.AssetType]);
                else panels[i].SetValues(thirdPartyColor, mugshotTable[node.Value.BattlerDef.AssetType]);
                if (node.Next != null) node = node.Next;
                else node = turnOrder.First;
            }
        }
    }
}