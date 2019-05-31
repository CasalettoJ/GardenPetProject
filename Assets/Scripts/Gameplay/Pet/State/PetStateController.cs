using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PetProject
{
    [System.Serializable]
    [RequireComponent(typeof(PetStats))]
    [RequireComponent(typeof(PetAbilities))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class PetStateController : StateController<PetStateController>
    {
        [Header("Animator")]
        public string MovementSpeedName = "MovementSpeed";
        public string AnimationStateName = "AnimationState";
        public int NoneAnimationState = 0;
        public int IdlingAnimationState = 1;
        public int GroundedMovementAnimationState = 2;
        public int AirMovementAnimationState = 3;
        public int WaterMovementAnimationState = 4;

        public Animator Animator { get; private set; }
        public PetStats Stats { get; private set; }
        public PetAbilities Abilities { get; private set; }
        public NavMeshAgent NavMeshAgent { get; private set; }
        [HideInInspector] public Vector3 TargetMovementPoint;


        [Header("AI Settings")]
        public float SightRadius = 10.0f;
        public float SecondsOfThought = 4;

        public override void Start()
        {
            Stats = GetComponent<PetStats>();
            Abilities = GetComponent<PetAbilities>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            Animator.SetInteger(AnimationStateName, NoneAnimationState);
            base.Start();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Animator.SetFloat(MovementSpeedName, NavMeshAgent.speed);
        }
    }
}