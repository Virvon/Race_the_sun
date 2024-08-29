using Assets.RaceTheSun.Sources.GameLogic.Attachment;
using Assets.RaceTheSun.Sources.Gameplay.DistanceObserver;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.StageInfo;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.Tiles
{
    public class StartStageTile : Tile, IObserver
    {
        private const string Title = "Уровень";

        [SerializeField] private TMP_Text _stageNumber;
        [SerializeField] private bool _needStageNumber;

        private CurrentSpaceshipStage _currentSpacehsipStage;
        private DistanceObservable _distanceObservable;
        private Stage _stage;
        private int _currentStageNumber;
        private AttachmentCellsUpgrader _attachmentCellsUpgrader;

        [Inject]
        private void Construct(
            DistanceObservable distanceObservable,
            CurrentSpaceshipStage currentSpacehsipStage,
            CurrentGenerationStage currentGenerationStage,
            AttachmentCellsUpgrader attachmentCellsUpgrader)
        {
            _currentSpacehsipStage = currentSpacehsipStage;
            _distanceObservable = distanceObservable;
            _currentStageNumber = currentGenerationStage.CurrentStageNumber;
            _attachmentCellsUpgrader = attachmentCellsUpgrader;

            _stage = currentGenerationStage.GeneratedStageType;

            if (_needStageNumber)
                _stageNumber.text = $"{Title} {_currentStageNumber}";
        }

        private void Start() =>
            _distanceObservable.RegisterObserver(this, transform.position);

        public virtual void Invoke()
        {
            _currentSpacehsipStage.SetCurrentStage(_stage);
            _attachmentCellsUpgrader.TryUpgrade(_currentStageNumber);
        }
    }
}
