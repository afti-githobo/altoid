using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Altoid.Battle.Types.Action;

namespace Altoid.Battle.Frontend
{
    public class ActionMenuItem : MonoBehaviour
    {
        static readonly int HASH_NONHOVER = Animator.StringToHash("NonHover");
        static readonly int HASH_HOVER = Animator.StringToHash("Hover");
        static readonly int HASH_SELECT = Animator.StringToHash("HOVER");

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Image ActionIcon;
        [SerializeField]
        private TextMeshProUGUI Tag;
        public ActionMenuItem NextUp { get => _nextUp; }
        [SerializeField]
        private ActionMenuItem _nextUp;
        public ActionMenuItem NextDown { get => _nextDown; }
        [SerializeField]
        private ActionMenuItem _nextDown;
        public ActionMenuItem NextLeft { get => _nextLeft; }
        [SerializeField]
        private ActionMenuItem _nextLeft;
        public ActionMenuItem NextRight { get => _nextRight; }
        [SerializeField]
        private ActionMenuItem _nextRight;

        private BattleAction associatedAction;

        public void SetAction(BattleAction action)
        {
            associatedAction = action;
            if (action == null) gameObject.SetActive(false);
            else
            {
                gameObject.SetActive(true);
                // action icon
            }
        }

        public void Hover()
        {
            // todo: localize
            BattleFrontendMain.Instance.BattleTextBox.Display(associatedAction.Description);
            animator.Play(HASH_HOVER);
        }

        public void NonHover()
        {
            animator.Play(HASH_NONHOVER);
        }

        public void Select()
        {
            animator.Play(HASH_SELECT);
        }
    }
}