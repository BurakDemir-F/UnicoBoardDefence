using GamePlay.Map;
using UnityEngine;

namespace GamePlay.Areas.Trigger
{
    public class TriggerMapConnector : MonoBehaviour
    {
        [SerializeField] private MapSO _map;
        private ITriggerBox _triggerBox;

        private void Awake()
        {
            _triggerBox = GetComponent<ITriggerBox>();
            _map.MapInitialized += MapInitialized;
        }

        private void OnDestroy()
        {
            _map.MapInitialized -= MapInitialized;
            _map.RemoveTriggerBox(_triggerBox);
        }

        private void MapInitialized()
        {
            _map.AddTriggerBox(_triggerBox);
        }
    }
}