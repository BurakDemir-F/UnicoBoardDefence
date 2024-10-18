using GamePlay.Map;
using UnityEngine;

namespace GamePlay.Areas
{
    public class TriggerMapConnector : MonoBehaviour
    {
        [SerializeField] private MapSO _map;
        private ITriggerBox _triggerBox;

        private void Awake()
        {
            _triggerBox = GetComponent<ITriggerBox>();
            _map.AddTriggerBox(_triggerBox);
        }
    }
}