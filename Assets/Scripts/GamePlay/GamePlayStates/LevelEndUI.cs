using TMPro;
using UnityEngine;

namespace GamePlay.GamePlayStates
{
    public class LevelEndUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelEndText;
        public void Activate()
        {
            _levelEndText.gameObject.SetActive(true);
        }
        
        public void Deactivate()
        {
            _levelEndText.gameObject.SetActive(false);
        }

    }
}