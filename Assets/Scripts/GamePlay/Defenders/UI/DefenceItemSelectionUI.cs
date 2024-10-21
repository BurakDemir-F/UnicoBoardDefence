using System.Collections.Generic;
using GamePlay.Areas;
using GamePlay.EventBus;
using GamePlay.EventBus.Info;
using General;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace GamePlay.Defenders.UI
{
    public class DefenceItemSelectionUI : MonoBehaviour
    {
        [SerializeField] private List<ItemUI> _itemUis;
        [SerializeField] private ItemActionsEventBus _itemActions;
        [SerializeField] private GameItemsSO _gameItems;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _closeButton;
        
        private DefenderArea _defenderArea;

        public void Initialize(DefenderArea area)
        {
            _defenderArea = area;
        }
        
        private void Awake()
        {
            foreach (var itemUI in _itemUis)
            {
                itemUI.DefenceItemSelected += OnDefenceItemSelected;
            }
            _itemActions.Subscribe(ItemActions.RemainingDefenceItemsChanged,OnRemainingItemsChanged);
            _canvas.worldCamera = Camera.main;
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDestroy()
        {
            foreach (var itemUI in _itemUis)
            {
                itemUI.DefenceItemSelected -= OnDefenceItemSelected;
            }
            _itemActions.UnSubscribe(ItemActions.RemainingDefenceItemsChanged,OnRemainingItemsChanged);
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        private void OnDisable()
        {
            Deactivate();
        }

        public void Activate()
        {
            ActivateUI();
        }

        public void Deactivate()
        {
            DeactivateUI();
        }

        private void ActivateUI()
        {
            foreach (var itemUi in _itemUis)
            {
                itemUi.Activate();
            }
            _closeButton.gameObject.SetActive(true);
        }

        private void DeactivateUI()
        {
            foreach (var itemUi in _itemUis)
            {
                itemUi.Deactivate();
            }
            _closeButton.gameObject.SetActive(false);
        }

        private void OnCloseButtonClick()
        {
            Deactivate();
        }

        private void OnDefenceItemSelected(DefenderType defenderType)
        {
            _itemActions.Publish(ItemActions.DefenceItemSelected,
                new DefenceItemSelectedEventInfo(defenderType, _defenderArea));
            DeactivateUI();
        }

        private void OnRemainingItemsChanged(IEventInfo info)
        {
            var remainingInfo = (RemainingItemsInfo)info;
            var itemDict = remainingInfo.RemainingDefenceItems;
            UpdateUI(itemDict);
        }

        private void UpdateUI(Dictionary<DefenderType,int> itemDict)
        {
            if (itemDict.Count > _itemUis.Count)
            {
                "Item Selection UI Something Wrong Here!".PrintColored(Color.red);
                return;
            }
            
            var index = 0;
            foreach (var (defenderType, count) in itemDict)
            {
                var ui = _itemUis[index];
                var defenderSprite = _gameItems.DefenderItemProperties[defenderType].Visual;
                ui.UpdateUI(defenderType,count,defenderSprite);
                index++;
            }
        }
    }
}