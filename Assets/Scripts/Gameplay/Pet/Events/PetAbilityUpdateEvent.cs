using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public static partial class GameEventLibrary { public static PetAbilityUpdateEvent PetAbilityUpdateEvent = new PetAbilityUpdateEvent(); }

    public struct PetAbilityUpdateArgs
    {
        public PetAbilityUpdateArgs(PetAbility o, PetAbility n, GameObject p, GameObject s)
        {
            OldAbility = o;
            NewAbility = n;
            Pet = p;
            Source = s;
        }
        public PetAbility OldAbility { get; private set; }
        public PetAbility NewAbility { get; private set; }
        public GameObject Pet { get; private set; }
        public GameObject Source { get; private set; }
    }

    public class PetAbilityUpdateEvent : GameEvent<PetAbilityUpdateArgs> { }
}