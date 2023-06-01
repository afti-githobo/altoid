using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Frontend
{
    public class ActionMenuItemPostSelectAnimBehavior : StateMachineBehaviour
    {
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var item = animator.gameObject.GetComponent<ActionMenuItem>();
            BattleFrontendMain.Instance.ActionMenu.ActionSelected(item);
        }
    }
}