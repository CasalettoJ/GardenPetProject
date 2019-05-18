using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    [CreateAssetMenu(fileName ="LockOnToBrowse",menuName ="StateMachines/Cursor/Transitions/LockOnToBrowse")]
    public class LockOnToBrowse : StateTransitionCondition<CursorStateController>
    {
        [Header("Settings")]
        public string exitLockOnButtonInputName;

        public override string DetermineNextState(CursorStateController stateController)
        {
            if (Input.GetButtonDown(exitLockOnButtonInputName) && State != null)
            {
                return State.StateName;
            }
            return "";
        }
    }
}