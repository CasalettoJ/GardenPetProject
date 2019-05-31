using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    [CreateAssetMenu(fileName = "PetIdleState", menuName = "StateMachines/Pet/States/PetIdleState")]
    public class PetIdleState : State<PetStateController>
    {
        public override void OnEnter(PetStateController controller)
        {
            controller.Animator.SetInteger(controller.AnimationStateName, controller.IdlingAnimationState);
        }
    }
}