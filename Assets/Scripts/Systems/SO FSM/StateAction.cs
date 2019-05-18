using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public abstract class StateAction<T> : ScriptableObject where T : StateController<T>
    {
        public abstract void Act(T stateController);
    }

}