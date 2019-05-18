using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    [CreateAssetMenu(fileName = "BrowseToLockOn", menuName = "StateMachines/Cursor/Transitions/BrowseToLockOn")]
    public class BrowseToLockOn : StateTransitionCondition<CursorStateController>
    {
        [Header("Settings")]
        public string enterLockOnButtonInputName;

        public override string DetermineNextState(CursorStateController stateController)
        {
            if (State != null && Input.GetButtonDown(enterLockOnButtonInputName) && stateController.highlightedObj != null)
            {
                return State.StateName;
            }
            return "";
        }
    }
}