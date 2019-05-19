using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PetProject
{
    [CreateAssetMenu(fileName = "IdleToWander", menuName = "StateMachines/Pet/Transitions/IdleToWander")]
    public class IdleToWander : StateTransitionCondition<PetStateController>
    {
        [Header("Settings")]
        public int PercentChanceToWander = 35;

        public override string DetermineNextState(PetStateController stateController)
        {
            throw new System.NotImplementedException();
        }
    }
}