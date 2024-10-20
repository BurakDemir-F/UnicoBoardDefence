using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Defenders.UI
{
    public class DefenceItemUIController : MonoBehaviour,IPointerDownHandler
    {
        [SerializeField] private DefenceItemSelectionUI _selectionUI;
        private void Awake()
        {
            _selectionUI.Deactivate();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _selectionUI.Activate();
        }
    }
}