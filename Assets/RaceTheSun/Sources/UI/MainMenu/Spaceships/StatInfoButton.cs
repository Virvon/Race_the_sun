using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public abstract class StatInfoButton : InformationButton
    {
        [SerializeField] private SpaceshipStatPanel _statPanel;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistentProgressService persistentProgressService)
        {
            StaticDataService = staticDataService;
            PersistentProgressService = persistentProgressService;

            _statPanel.Updated += OnStatPanelUpdated;
        }

        protected IStaticDataService StaticDataService { get; private set; }
        protected IPersistentProgressService PersistentProgressService { get; private set; }
        protected SpaceshipType CurrentSpaceshipType => _currentClickedSpaceshipInfo.SpaceshipType;

        private void OnDestroy()
        {
            _statPanel.Updated -= OnStatPanelUpdated;
        }

        public override void OpenInfo()
        {
            Info.text = GetInfo();
            base.OpenInfo();
        }

        private void OnStatPanelUpdated() =>
            HideInfo();

        protected abstract string GetInfo();
    }
}