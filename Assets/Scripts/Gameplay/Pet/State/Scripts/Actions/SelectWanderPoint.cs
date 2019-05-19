using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PetProject
{
    [CreateAssetMenu(fileName = "SelectWanderPoint", menuName = "StateMachines/Pet/Actions/SelectWanderPoint")]
    public class SelectWanderPoint : StateAction<PetStateController>
    {
        public override void Act(PetStateController stateController)
        {
            if (!stateController.NavMeshAgent.hasPath)
            {
                SetNewDestination(stateController);
            }
        }

        private void SetNewDestination(PetStateController stateController)
        {
            // https://gist.github.com/IJEMIN/f2510a85b1aaf3517da1af7a6f9f1ed3
            Vector3 randomPos = Random.insideUnitSphere * stateController.SightRadius + stateController.m_transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPos, out hit, stateController.SightRadius, NavMesh.AllAreas);
            stateController.TargetMovementPoint = hit.position;
        }
    }
}