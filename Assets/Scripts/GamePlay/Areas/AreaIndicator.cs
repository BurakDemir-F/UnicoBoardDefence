using UnityEngine;

namespace GamePlay.Areas
{
    public class AreaIndicator : MonoBehaviour, IAreaIndicator
    {
        [SerializeField] private GameObject _buildIndicator;
        [SerializeField] private GameObject _rangeIndicator;

        public void ActivateBuildIndicator()
        {
            _buildIndicator.SetActive(true);
        }

        public void DeactivateBuildIndicator()
        {
            _buildIndicator.SetActive(false);
        }

        public void ActivateRangeIndicator()
        {
            _rangeIndicator.SetActive(true);
        }

        public void DeactivateRangeIndicator()
        {
            _rangeIndicator.SetActive(false);
        }

        public void DeactivateIndicator()
        {
            DeactivateBuildIndicator();
            DeactivateRangeIndicator();
        }
    }

    public interface IAreaIndicator
    {
        void ActivateBuildIndicator();
        void DeactivateBuildIndicator();
        void ActivateRangeIndicator();
        void DeactivateRangeIndicator();
        void DeactivateIndicator();
    }
}