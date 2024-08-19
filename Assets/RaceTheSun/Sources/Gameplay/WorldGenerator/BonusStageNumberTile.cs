using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class BonusStageNumberTile : Tile
    {
        private const string Title = "Уровень";

        [SerializeField] private TMP_Text _stageNumber;

        [Inject]
        private void Construct(CurrentGenerationStage currentGenerationStage)
        {
            

            _stageNumber.text = $"{Title} {currentGenerationStage.CurrentStageNumber}";
        }
    }
}
