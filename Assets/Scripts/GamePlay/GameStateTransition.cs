using System;
using General.StateMachines;
using UnityEngine;

namespace GamePlay
{
    public class GameStateTransition : MonoBehaviour,ITransition
    {
        public IState FromState { get; set; }
        public IState ToState { get; set; }


        public virtual void MakeTransition(Action OnTransitionEnd)
        {
            OnTransitionEnd?.Invoke();
        }
    }
}