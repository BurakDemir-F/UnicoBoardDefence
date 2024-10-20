using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Enemies
{
    public class HealthUI : MonoBehaviour,IHealthUI
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _healthBar;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        public void SetHealth(float healthNormalized)
        {
            _healthBar.fillAmount = healthNormalized;
        }
    }

    public interface IHealthUI
    {
        void SetHealth(float healthNormalized);
    }
}