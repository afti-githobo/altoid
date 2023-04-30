using Altoid.Battle.Types.Action;
using Altoid.Battle.Types.Battlers;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Frontend
{
    public class ActionMenu : MonoBehaviour
    {
        public IReadOnlyList<ActionMenuItem> MenuItemsForStanceActions { get => _menuItemsForStanceActions; }
        private ActionMenuItem[] _menuItemsForStanceActions;
        public ActionMenuItem MenuItemForUltimate  { get => _menuItemForUltimate; }
        [SerializeField]
        private ActionMenuItem _menuItemForUltimate;
        public ActionMenuItem MenuItemForRun { get => _menuItemForRun; }
        [SerializeField]
        private ActionMenuItem _menuItemForRun;
        public ActionMenuItem MenuItemForWait { get => _menuItemForWait; }
        [SerializeField]
        private ActionMenuItem _menuItemForWait;
        public ActionMenuItem MenuItemForBreak { get => _menuItemForBreak; }
        [SerializeField]
        private ActionMenuItem _menuItemForBreak;

        [SerializeField]
        private BattleAction actionRun;
        [SerializeField]
        private BattleAction actionBreak;
        [SerializeField]
        private BattleAction actionWait;

        private ActionMenuItem selected;

        private void Awake()
        {
            MenuItemForRun.SetAction(actionRun);
            MenuItemForBreak.SetAction(actionBreak);
            MenuItemForWait.SetAction(actionWait);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (selected.NextUp.gameObject.activeSelf == true) Hover(selected.NextUp);
                else
                {
                    // this will hang if there isn't a selectable button somewhere - make sure they loop around
                    while (selected.NextUp.gameObject.activeSelf != true) selected = selected.NextUp;
                    Hover(selected);
                }   
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (selected.NextDown.gameObject.activeSelf == true) Hover(selected.NextDown);
                else
                {
                    // this will hang if there isn't a selectable button somewhere - make sure they loop around
                    while (selected.NextDown.gameObject.activeSelf != true) selected = selected.NextDown;
                    Hover(selected);
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (selected.NextLeft.gameObject.activeSelf == true) Hover(selected.NextLeft);
                else
                {
                    // this will hang if there isn't a selectable button somewhere - make sure they loop around
                    while (selected.NextLeft.gameObject.activeSelf != true) selected = selected.NextLeft;
                    Hover(selected);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (selected.NextRight.gameObject.activeSelf == true) Hover(selected.NextRight);
                else
                {
                    // this will hang if there isn't a selectable button somewhere - make sure they loop around
                    while (selected.NextRight.gameObject.activeSelf != true) selected = selected.NextRight;
                    Hover(selected);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                selected.Select();
            }
        }

        public void Hover (ActionMenuItem item)
        {
            selected = item;
            item.Hover();
            selected.NonHover();
        }

        public void SelectActionFor(Battler battler)
        {
            gameObject.SetActive(true);
            selected = _menuItemsForStanceActions[0];
            selected.Hover();
            if (battler.Stance == StanceType.StanceDropped || battler.Stance == StanceType.StanceForceBroken)
            {
                _menuItemForBreak.gameObject.SetActive(false);
                _menuItemForUltimate.gameObject.SetActive(false);
                for (int i = 0; i < _menuItemsForStanceActions.Length; i++)
                {
                    var stance = battler.PlayerStances[battler.PlayerStanceTypes[i]];
                    _menuItemsForStanceActions[i].SetAction(stance.StanceEntryAction);
                }
            }
            else
            {
                var stance = battler.PlayerStances[battler.Stance];
                if (battler.EntropyLevel == Constants.ENTROPY_INFINITE) _menuItemForUltimate.SetAction(stance.StanceUltimate);
                {
                    for (int i = 0; i < _menuItemsForStanceActions.Length; i++)
                    {
                        _menuItemsForStanceActions[i].SetAction(stance.StandardActions[i]);
                    }
                }
            }
        }
    }
}