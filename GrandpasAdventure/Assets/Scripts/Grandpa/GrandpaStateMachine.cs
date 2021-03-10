using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrandpaStates
{
    public class GrandpaStateMachine<T>
    {
        public State<T> currentState { get; private set; }
        public T owner;

        public GrandpaStateMachine(T _o)
        {
            owner = _o;
            currentState = null;
        }

        public void ChangeState(State<T> _newState)
        {
            if (currentState != null)
                currentState.ExitState(owner);
            currentState = _newState;
            currentState.EnterState(owner);
            
        }

        public void Update()
        {
            if (currentState != null)
                currentState.UpdateState(owner);
        }
    }

    public abstract class State<T>
    {
        public abstract void EnterState(T _owner);
        public abstract void ExitState(T _owner);
        public abstract void UpdateState(T _owner);
                    
    }
}
