using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PetProject
{
    [CreateAssetMenu(fileName = "WanderToPoint", menuName = "StateMachines/Pet/Actions/WanderToPoint")]
    public class WanderToPoint : StateAction<PetStateController>
    {
        public override void Act(PetStateController stateController)
        { 
            if (stateController.NavMeshAgent.destination != stateController.TargetMovementPoint)
            {
                MoveToDestination(stateController);
            }
        }

        private void MoveToDestination(PetStateController stateController)
        {
            stateController.NavMeshAgent.SetDestination(stateController.TargetMovementPoint);
        }
    }
}