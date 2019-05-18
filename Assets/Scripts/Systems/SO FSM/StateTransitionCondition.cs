using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public abstract class StateTransitionCondition<T> : ScriptableObject, ISerializationCallbackReceiver where T : StateController<T>
    {
        [Header("Settings")]
        [SerializeField]
        private ScriptableObject StateObject;
        public State<T> State;
        public abstract string DetermineNextState(T stateController);
        public virtual void OnValidate()
        {
            StateObject = StateObject is State<T> ? StateObject : null;
        }

        public void OnAfterDeserialize()
        {
            State = null;
            if (StateObject is State<T>)
            {
                State = StateObject as State<T>;
            }
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}
