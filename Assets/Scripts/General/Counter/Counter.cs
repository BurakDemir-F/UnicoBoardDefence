using System;
using General.Behaviours;
using UnityEngine;

namespace General.Counter
{
    public class Counter : MonoBehaviour,ICounter
    {
        public CounterVo Count(float duration, Action<float> onUpdateAction, Action onFinishAction,CounterType counterType = CounterType.Once)
        {
            var counterVo = new CounterVo()
                { Duration = duration, CounterType = counterType, UpdateAction = onUpdateAction ,FinishAction = onFinishAction};
            CounterSingleton.Instance.AddCounter(counterVo);
            return counterVo;
        }

        public void Remove(CounterVo vo)
        {
            CounterSingleton.Instance.RemoveCounter(vo);
        }
    }

    public interface ICounter
    {
        CounterVo Count(float duration, Action<float> onUpdateAction,Action onFinishAction = null,CounterType counterType = CounterType.Once);
        void Remove(CounterVo vo);
    }

    public enum CounterType
    {
        None,
        Once,
        Repeat
    }

    public class CounterVo
    {
        public float Duration;
        public float Counter;
        public Action<float> UpdateAction;
        public Action FinishAction;
        public CounterType CounterType;
        public bool IsFinished() => Counter >= Duration && Counter != 0f;
        public void InvokeUpdateAction(float value) => UpdateAction?.Invoke(value);
        public void InvokeFinishAction() => FinishAction?.Invoke();
        public void Refresh()
        {
            Counter = 0f;
        }
    }
}