using General;
using UnityEngine;

namespace GamePlay.EventBus
{
    [CreateAssetMenu(menuName = "ScriptableData/GamePlayEventBus", fileName = "GamePlayEventBus", order = 0)]
    public class GamePlayEventBus : EventBus<GamePlayEvent>
    {
        
    }
}