using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public abstract class StateController<T> : MonoBehaviour, ISerializationCallbackReceiver where T: StateController<T>
    {

        [Header("States")]
        [Tooltip("This list will only accept unique State ScriptableObjects.")]
        [SerializeField]
        private List<ScriptableObject> States = new List<ScriptableObject>();

        protected State<T> _currentState; // TODO: May have to serialize this.
        protected Dictionary<string, State<T>> StateMap = new Dictionary<string, State<T>>();

        [HideInInspector] public float DeltaTime = 0.0f;
        [HideInInspector] public float FixedDeltaTime = 0.0f;
        public Transform m_transform { get; private set; }
        public string CurrentStateName => _currentState == null ? "" : _currentState.StateName;

        public virtual void Start()
        {
            m_transform = transform;
            if (States.Count > 0)
            {
                StateTransition(((State<T>)States[0]).StateName);
            }
        }

        public virtual void ConditionCheck()
        {
            if (_currentState != null)
            {
                foreach (StateTransitionCondition<T> condition in _currentState.TransitionConditions)
                {
                    string nextState = condition.DetermineNextState(this as T);
                    if (nextState != null)
                    {
                        StateTransition(nextState);
                        break;
                    }
                }
            }

        }

        public virtual void Update()
        {
            DeltaTime = Time.deltaTime;
            if (_currentState != null)
            {
                _currentState.Tick(this as T);
            }
            ConditionCheck();
        }

        public virtual void FixedUpdate()
        {
            FixedDeltaTime = Time.fixedDeltaTime;
            if (_currentState != null)
            {
                _currentState.FixedTick(this as T);
            }
        }

        public void StateTransition(string nextState)
        {
            if (!string.IsNullOrEmpty(nextState) &&
                (_currentState == null || nextState != _currentState.StateName) &&
                StateMap.ContainsKey(nextState))
            {
                if (_currentState != null)
                {
                    _currentState.OnExit(this as T);
                }
                _currentState = StateMap[nextState];
                _currentState.OnEnter(this as T);
            }
        }

        public virtual void OnValidate()
        {
            List<ScriptableObject> newStateList = new List<ScriptableObject>(new ScriptableObject[States.Count]);
            for (int i = 0; i < States.Count; i++)
            {
                if (States[i] is State<T> && !newStateList.Contains(States[i]))
                {
                    newStateList[i] = States[i]; // retain order when validating and removing dupes.
                }
            }
            //https://stackoverflow.com/questions/466946/how-to-initialize-a-listt-to-a-given-size-as-opposed-to-capacity
            States = new List<ScriptableObject>(new ScriptableObject[newStateList.Count]);
            for (int i = 0; i < newStateList.Count; i++)
            {
                States[i] = newStateList[i]; // match the new index length while removing duplicate states.
            }
        }

        public virtual void OnBeforeSerialize()
        {

        }

        public virtual void OnAfterDeserialize()
        {
            int defaultKey = StateMap.Count + 1;
            StateMap.Clear();
            for (int i = 0; i < States.Count; i++)
            {
                ScriptableObject s = States[i];
                if (s == null)
                {
                    StateMap.Add((defaultKey++).ToString(), null);
                    continue;
                }
                if (!(s is State<T>))
                {
                    continue;
                }
                string stateName = ((State<T>)s).StateName;
                if (string.IsNullOrEmpty(stateName) || string.IsNullOrWhiteSpace(stateName))
                {
                    stateName = (defaultKey++).ToString();
                }
                if (s is State<T> && !StateMap.ContainsKey(stateName))
                {
                    StateMap.Add(stateName, s as State<T>);
                }
            }
        } 
    }
}