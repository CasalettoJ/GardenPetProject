using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public static partial class GameEventLibrary { public static PetNameChangeEvent PetNameChangeEvent = new PetNameChangeEvent(); }

    public struct PetNameChangeArgs
    {
        public PetNameChangeArgs(string o, string n, GameObject p)
        {
            OldName = o;
            NewName = n;
            Pet = p;
        }
        public string OldName { get; private set; }
        public string NewName { get; private set; }
        public GameObject Pet { get; private set; }
    }

    public class PetNameChangeEvent : GameEvent<PetNameChangeArgs> { }
}