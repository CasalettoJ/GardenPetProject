using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public static partial class GameEventLibrary { public static PetStatUpdateEvent PetStatUpdateEvent = new PetStatUpdateEvent(); }

    public struct PetStatUpdateArgs
    {
        public PetStatUpdateArgs(PetStatType t, int i, GameObject p, GameObject s)
        {
            Type = t;
            Amount = i;
            Pet = p;
            Source = s;
        }
        public int Amount { get; private set; }
        public GameObject Pet { get; private set; }
        public PetStatType Type { get; private set; }
        public GameObject Source { get; private set; }
    }

    public class PetStatUpdateEvent : GameEvent<PetStatUpdateArgs> {}
}