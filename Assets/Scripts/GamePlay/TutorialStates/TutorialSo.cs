using Defenders;
using UnityEngine;

namespace GamePlay.TutorialStates
{
    [CreateAssetMenu(menuName = "ScriptableData/Tutorial", fileName = "Tutorial", order = 0)]
    public class TutorialSo : ScriptableObject,ITutorialData
    {
        [SerializeField] private Vector2Int _tutorialDefenderLocation;
        [SerializeField] private DefenderType _tutorialDefenderType;
        [SerializeField] private string _tapDefenderText = "Tap To Build Defender";
        [SerializeField] private string _pickDefenderText = "Pick Defender";
        [SerializeField] private string _blueRectanglesInfo = "Blue Rectangles Indicates Range of The Defender";
        [SerializeField] private string _tapToBuildText = "Tap to Build";
        
        public Vector2Int TutorialDefenderLocation => _tutorialDefenderLocation;
        public DefenderType TutorialDefenderType => _tutorialDefenderType;
        public string TapDefenderText => _tapDefenderText;
        public string PickDefenderText => _pickDefenderText;
        public string BlueRectanglesInfo => _blueRectanglesInfo;
        public string TapToBuildText => _tapToBuildText;
    }

    public interface ITutorialData
    {
        Vector2Int TutorialDefenderLocation{get;}
        DefenderType TutorialDefenderType{get;}
        string TapDefenderText {get;}
        string PickDefenderText {get;}
        string BlueRectanglesInfo {get;}
        string TapToBuildText {get;}
    }
}