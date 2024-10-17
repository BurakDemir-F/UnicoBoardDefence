using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Map.MapGrid
{
    public class AreaAnimator : MonoBehaviour,IAreaAnimator
    {
        [SerializeField] private Transform _animationTransform;
        [SerializeField] private Vector2 _minMaxAngle;
        [SerializeField] private float _animDuration;

        private Action _onComplete;
        private AnimationTemp _animInfo;
        public void AnimatePlacing(Action onComplete)
        {
            _onComplete = onComplete;
            var randomXAngle = Random.Range(_minMaxAngle.x, _minMaxAngle.y);
            var randomZAngle = Random.Range(_minMaxAngle.x, _minMaxAngle.y);
            var targetRotation = Quaternion.Euler(randomXAngle, 0f, randomZAngle);

            _animInfo.TargetRotation = targetRotation;
            _animInfo.DefaultRotation = _animationTransform.rotation;
            _animInfo.IsAnimating = true;
        }

        private void Update()
        {
            if (!_animInfo.IsAnimating)
                return;
            var fromRotation = _animInfo.IsReturningToDefault ? _animInfo.TargetRotation : _animInfo.DefaultRotation; 
            var toRotation = _animInfo.IsReturningToDefault ? _animInfo.DefaultRotation : _animInfo.TargetRotation;
            var tValue = _animInfo.Counter / _animDuration;
            _animationTransform.rotation = Quaternion.Lerp(fromRotation, toRotation, tValue);

            _animInfo.Counter += Time.deltaTime;
            if (_animInfo.Counter >= _animDuration)
            {
                _animationTransform.rotation = _animInfo.DefaultRotation;
                _animInfo.Reset();
                _onComplete?.Invoke();
                return;
            }

            if (_animInfo.Counter > _animDuration / 2f)
            {
                _animInfo.IsReturningToDefault = true;
            }
        }

        private struct AnimationTemp
        {
            public bool IsAnimating;
            public bool IsReturningToDefault;
            public Quaternion TargetRotation;
            public Quaternion DefaultRotation;
            public float Counter;

            public void Reset()
            {
                IsAnimating = false;
                IsReturningToDefault = false;
                TargetRotation = default;
                DefaultRotation = default;
                Counter = 0f;
            }
        }
    }

    public interface IAreaAnimator
    {
        void AnimatePlacing(Action onComplete);
    }
}