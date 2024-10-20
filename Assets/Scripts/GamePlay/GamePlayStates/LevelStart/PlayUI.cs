using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.GamePlayStates
{
    public class PlayUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Button _playButton;
        public event Action PlayButtonClicked;

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            PlayButtonClicked?.Invoke();
        }

        public void Activate()
        {
            _playButton.gameObject.SetActive(true);
            _levelText.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _playButton.gameObject.SetActive(false);
            _levelText.gameObject.SetActive(false);
        }

        public void ShowLevel(int level)
        {
            _levelText.SetText($"Level {level.ToString()}");
        }
    }
}