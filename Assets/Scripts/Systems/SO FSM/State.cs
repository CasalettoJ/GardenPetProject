using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public abstract class State<T> : ScriptableObject, ISerializationCallbackReceiver where T : StateController<T>
    {
        [SerializeField]
        private string stateName;
        [SerializeField]
        private List<ScriptableObject> ActionObjects = new List<ScriptableObject>();
        [SerializeField]
        private List<ScriptableObject> FixedActionObjects = new List<ScriptableObject>();
        [SerializeField]
        private List<ScriptableObject> TransitionConditionObjects = new List<ScriptableObject>();
        public string StateName => stateName;

        protected bool _halted = false;
        public List<StateAction<T>> Actions { get; private set; } = new List<StateAction<T>>();
        public List<StateAction<T>> FixedActions { get; private set; } = new List<StateAction<T>>();
        public List<StateTransitionCondition<T>> TransitionConditions { get; private set; } = new List<StateTransitionCondition<T>>();

        public virtual void FixedTick(T controller)
        {
            FixedActions.ForEach(fa => { if (!_halted) { fa.Act(controller); } });
        }

        public virtual void Tick(T controller)
        {
            Actions.ForEach(a => { if (!_halted) { a.Act(controller); } });
        }

        public virtual void OnEnter(T controller) { _halted = false; }
        public virtual void OnExit(T controller) { _halted = true; }

        public virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(stateName))
            {
                stateName = Random.Range(0, int.MaxValue).ToString(); // Just set the name randomly if there isn't one.
            }
            for (int i = 0; i < ActionObjects.Count; i++)
            {
                ActionObjects[i] = ActionObjects[i] is StateAction<T> ? ActionObjects[i] : null;
            }
            for (int i = 0; i < FixedActionObjects.Count; i++)
            {
                FixedActionObjects[i] = FixedActionObjects[i] is StateAction<T> ? FixedActionObjects[i] : null;
            }
            for (int i = 0; i < TransitionConditionObjects.Count; i++)
            {
                TransitionConditionObjects[i] = TransitionConditionObjects[i] is StateTransitionCondition<T> ? TransitionConditionObjects[i] : null;
            }
        }

        public virtual void OnBeforeSerialize()
        {

        }

        public virtual void OnAfterDeserialize()
        {
            Actions.Clear();
            FixedActions.Clear();
            TransitionConditions.Clear();

            for (int i = 0; i < ActionObjects.Count; i++)
            {
                if (ActionObjects[i] is StateAction<T>)
                {
                    Actions.Add(ActionObjects[i] as StateAction<T>);
                }
            }
            for (int i = 0; i < FixedActionObjects.Count; i++)
            {
                if (FixedActionObjects[i] is StateAction<T>)
                {
                    FixedActions.Add(FixedActionObjects[i] as StateAction<T>);
                }
            }
            for (int i = 0; i < TransitionConditionObjects.Count; i++)
            {
                if (TransitionConditionObjects[i] is StateTransitionCondition<T>)
                {
                    TransitionConditions.Add(TransitionConditionObjects[i] as StateTransitionCondition<T>);
                }
            }
        }
    }
}