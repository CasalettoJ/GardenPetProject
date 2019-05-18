using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    [RequireComponent(typeof(PetStats))]
    [RequireComponent(typeof(PetAbilities))]
    public class PetStateController : StateController<PetStateController>
    {
        public PetStats Stats { get; private set; }
        public PetAbilities Abilities { get; private set; }

        public override void Start()
        {
            base.Start();
            Stats = GetComponent<PetStats>();
            Abilities = GetComponent<PetAbilities>();
        }
    }
}