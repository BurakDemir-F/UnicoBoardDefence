using System;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public abstract class EventBus<T> : ScriptableObject where T : System.Enum
    {
        private Dictionary<T,HashSet<Action<IEventInfo>>> _subscriberActions = new();
        private HashSet<Action<T, IEventInfo>> _actionSet = new();
        private event Action<T, IEventInfo> _action;

        public void Subscribe(T subscriptionType, Action<IEventInfo> function)
        {
            if (_subscriberActions.TryGetValue(subscriptionType, out var actionSet))
            {
                if (actionSet.Contains(function))
                    return;
                
                actionSet.Add(function);
            }
            else
            {
                _subscriberActions.Add(subscriptionType,new HashSet<Action<IEventInfo>>(){function});
            }
        }

        public void UnSubscribe(T subscriptionType, Action<IEventInfo> function)
        {
            if(!_subscriberActions.TryGetValue(subscriptionType,out var actionSet))
                return;
            
            if(!actionSet.Contains(function))
                return;

            actionSet.Remove(function);
        }

        public void Subscribe(Action<T,IEventInfo> function)
        {
            if (_actionSet.Contains(function))
                return;

            _actionSet.Add(function);
            _action += function;
        }

        public void UnSubscribe(Action<T,IEventInfo> function)
        {
            if (!_actionSet.Contains(function))
                return;

            _actionSet.Remove(function);
            _action -= function;
        }

        public void Publish(T parameterEnum,IEventInfo eventInfo)
        {
            if (_subscriberActions.TryGetValue(parameterEnum, out var actions))
            {
                foreach (var action in actions)
                {
                    action?.Invoke(eventInfo);
                }
            }
            
            _action?.Invoke(parameterEnum,eventInfo);
        }
    }

    public interface IEventInfo
    {
        
    }
}