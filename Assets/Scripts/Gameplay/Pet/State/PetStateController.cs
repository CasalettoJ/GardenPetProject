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
        public string IsMovingStateName = "IsMoving";
        public string AnimationStateName = "AnimationState";
        public int NoneAnimationState = 0;
        public int IdlingAnimationState = 1;
        public int GroundedMovementAnimationState = 2;
        public int AirMovementAnimationState = 3;
        public int WaterMovementAnimationState = 4;
        public float GroundOrientationLerp = 10f;

        public Animator Animator { get; private set; }
        public PetStats Stats { get; private set; }
        public PetAbilities Abilities { get; private set; }
        public NavMeshAgent NavMeshAgent { get; private set; }
        [HideInInspector] public Vector3 TargetMovementPoint;
        [HideInInspector] public bool IsMoving;


        [Header("AI Settings")]
        public float SightRadius = 10.0f;
        public float SecondsOfThought = 4;

        private Vector3 _prevPos;

        public override void Start()
        {
            Stats = GetComponent<PetStats>();
            Abilities = GetComponent<PetAbilities>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            Animator.SetInteger(AnimationStateName, NoneAnimationState);
            base.Start();
        }

        public override void Update()
        {
            IsMoving = _prevPos != m_transform.position;
            Animator.SetBool(IsMovingStateName, IsMoving);
            _prevPos = m_transform.position;
            base.Update();
            OrientToGround();
        }

        private void OrientToGround()
        {
            //https://stackoverflow.com/questions/53117631/navmeshagent-player-not-parallel-to-slope-of-hill-when-moving-over-hill
            RaycastHit slopeHit;

            //Perform raycast from the object's position downwards
            if (Physics.Raycast(m_transform.position, Vector3.down, out slopeHit, 100.0f, LayerMask.GetMask("Cursor_Layer")))
            {
                //Drawline to show the hit point
                Debug.DrawLine(m_transform.position, slopeHit.point, Color.red);

                //Get slope angle from the raycast hit normal then calcuate new pos of the object
                Quaternion newRot = Quaternion.FromToRotation(m_transform.up, slopeHit.normal)
                    * m_transform.rotation;

                //Apply the rotation 
                m_transform.rotation = Quaternion.Lerp(m_transform.rotation, newRot,
                    Time.deltaTime * GroundOrientationLerp);

            }
        }
    }
}