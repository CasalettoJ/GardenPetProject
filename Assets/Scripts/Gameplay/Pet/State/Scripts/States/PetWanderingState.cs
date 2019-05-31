using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    [CreateAssetMenu(fileName = "PetWanderingState", menuName = "StateMachines/Pet/States/PetWanderingState")]
    public class PetWanderingState : State<PetStateController>
    {
        public override void OnEnter(PetStateController controller)
        {
            controller.Animator.SetInteger(controller.AnimationStateName, controller.GroundedMovementAnimationState);
        }
    }
}