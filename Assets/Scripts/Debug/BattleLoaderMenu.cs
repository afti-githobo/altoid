using System;
using UnityEngine;
using TMPro;
using Altoid.Battle.Types.Battle;

namespace Altoid.Debug
{
    public class BattleLoaderMenu : MonoBehaviour
    {
        const string BATTLE_DEF_PATH = "Data/Battle/BattleDef";
        static BattleDef[] battleDefs;

        [SerializeField]
        private TMP_Dropdown BattleSelect;

        [SerializeField]
        private TextMeshProUGUI DescriptionField;

        void Awake()
        {
            PopulateDropdown();
        }

        void Update()
        {
            PopulateElements();
        }

        private static void PopulateBattleDefs()
        {
            battleDefs = Resources.LoadAll<BattleDef>(BATTLE_DEF_PATH);
        }

        private void PopulateDropdown()
        {
            if (battleDefs == null) PopulateBattleDefs();
            for (int i = 0; i < battleDefs.Length; i++)
            {
                BattleSelect.options.Add(new TMP_Dropdown.OptionData($"{i}: " + battleDefs[i].name));
            }
        }

        private void PopulateElements()
        {
            var battle = battleDefs[BattleSelect.value];
            DescriptionField.text = battle.Description;
        }

        public void StartBattle()
        {
            throw new NotImplementedException();
        }
    }

}