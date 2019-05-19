using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PetProject
{
    [RequireComponent(typeof(PetStats))]
    [RequireComponent(typeof(PetAbilities))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class PetStateController : StateController<PetStateController>
    {
        public PetStats Stats { get; private set; }
        public PetAbilities Abilities { get; private set; }
        public NavMeshAgent NavMeshAgent { get; private set; }

        [HideInInspector] public Vector3 TargetMovementPoint;
        public float SightRadius = 10.0f;
        public float SecondsOfThought = 4;

        public override void Start()
        {
            base.Start();
            Stats = GetComponent<PetStats>();
            Abilities = GetComponent<PetAbilities>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
}