using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class InteractionController: MonoBehaviour,IInteractionController
    {
        [SerializeField] private Graphic _eventCatcher;

        private void Awake()
        {
            EnableInteraction();
        }

        public void EnableInteraction()
        {
            _eventCatcher.raycastTarget = false;
        }

        public void DisableInteraction()
        {
            _eventCatcher.raycastTarget = true;
        }
    }

    public interface IInteractionController
    {
        void EnableInteraction();
        void DisableInteraction();
    }
}