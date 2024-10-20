using GamePlay;
using GamePlay.Areas;
using General;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Defenders.UI
{
    public class DefenceItemUIController : MonoBehaviour,IPointerDownHandler
    {
        [SerializeField] private DefenderArea _defenderArea;
        [SerializeField] private DefenceItemSelectionUI _selectionUI;
        [SerializeField] private LevelSO _level;
        [SerializeField] private GamePlayEventBus _eventBus;
        [SerializeField] private float _xDistanceToCamera;
        private Camera _cam;
        private void Awake()
        {
            _selectionUI.Initialize(_defenderArea);
            _selectionUI.Deactivate();
            _cam = Camera.main;
            _eventBus.Subscribe(GamePlayEvent.MapCreated,OnMapCreated);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe(GamePlayEvent.MapCreated,OnMapCreated);
        }

        private void OnMapCreated(IEventInfo obj)
        {
            Locate();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(_defenderArea.IsPlaced)
                return;
            
            _selectionUI.Activate();
        }
        
        private void Locate()
        {
            var uiPos = _selectionUI.transform.position;
            var camPos = _cam.transform.position;
            camPos.SetY(uiPos.y);
            camPos.SetZ(uiPos.z);
            if(Vector3.Magnitude(uiPos - camPos) < _xDistanceToCamera)
                return;

            if (camPos.x > uiPos.x)
            {
                _selectionUI.transform.position += new Vector3(_xDistanceToCamera,0f,0f);
            }
            else
            {
                _selectionUI.transform.position -= new Vector3(_xDistanceToCamera,0f,0f);
            }
        }
    }
}