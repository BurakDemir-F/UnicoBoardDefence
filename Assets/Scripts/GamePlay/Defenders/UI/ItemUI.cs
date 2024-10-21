using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Defenders.UI
{
    [System.Serializable]
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _amountText;

        private DefenderType _type;
        public DefenderType ItemType => _type;
        public event Action<DefenderType> DefenceItemSelected;

        private Camera _cam;
        
        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
            _cam = Camera.main;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
        
        public void Activate()
        {
            _button.gameObject.SetActive(true);
            _itemImage.gameObject.SetActive(true);
            _amountText.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _button.gameObject.SetActive(false);
            _itemImage.gameObject.SetActive(false);
            _amountText.gameObject.SetActive(false);
        }

        public void UpdateUI(DefenderType type, int count,Sprite defenderSprite)
        {
            _type = type;
            _button.interactable = count > 0;
            _amountText.SetText(count.ToString());
            _itemImage.sprite = defenderSprite;
        }

        private void OnButtonClick()
        {
            DefenceItemSelected?.Invoke(_type);
        }
    }
}