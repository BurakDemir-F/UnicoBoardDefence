using System;
using General.StateMachines;
using UnityEngine;

namespace GamePlay
{
    public abstract class GameStateTransition : MonoBehaviour,ITransition
    {
        public IState FromState { get; set; }
        public IState ToState { get; set; }


        public void MakeTransition(Action OnTransitionEnd)
        {
            
        }
    }
}