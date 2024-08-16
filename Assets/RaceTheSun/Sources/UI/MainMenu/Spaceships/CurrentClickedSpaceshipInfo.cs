using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class CurrentClickedSpaceshipInfo : MonoBehaviour
    {
        [SerializeField] private CurrentClickedSpacehipWatcher _currentClickedSpaceshipWatcher;
        [SerializeField] private SpaceshipStatPanel[] _spaceshipStatPanels;
        [SerializeField] private TMP_Text _spaceshipName;
        [SerializeField] private TMP_Text _title;

        private IPersistentProgressService _persistentProgressService;
        private ModelPoint _modelPoint;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, ModelPoint modelPoint, IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;
            _modelPoint = modelPoint;
            _staticDataService = staticDataService;
        }

        public SpaceshipType SpaceshipType { get; private set; }

        private void OnEnable() =>
            _currentClickedSpaceshipWatcher.CurrentSpaceshipChanged += OnSpaceshipChanged;

        private void OnDisable() =>
            _currentClickedSpaceshipWatcher.CurrentSpaceshipChanged -= OnSpaceshipChanged;

        private async void OnSpaceshipChanged(SpaceshipType type)
        {
            SpaceshipType = type;

            _spaceshipName.text = _staticDataService.GetSpaceship(type).Name;
            _title.text = _staticDataService.GetSpaceship(type).Title;

            foreach (SpaceshipStatPanel spaceshipStatPanel in _spaceshipStatPanels)
                spaceshipStatPanel.ResetSpaceship(type);

            await _modelPoint.Change(type);
        }
    }
}