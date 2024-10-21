using GamePlay.Areas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay.Defenders.UI
{
    public class DefenceItemUIController : MonoBehaviour,IPointerDownHandler
    {
        [SerializeField] private DefenderArea _defenderArea;
        [SerializeField] private DefenceItemSelectionUI _selectionUI;
        [SerializeField] private LevelSO _level;
        [SerializeField] private float _xDistanceToCamera;
        private Camera _cam;
        private void Awake()
        {
            _selectionUI.Initialize(_defenderArea);
            _selectionUI.Deactivate();
            _cam = Camera.main;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if(_defenderArea.IsPlaced)
                return;
            
            _selectionUI.Activate();
        }
    }
}