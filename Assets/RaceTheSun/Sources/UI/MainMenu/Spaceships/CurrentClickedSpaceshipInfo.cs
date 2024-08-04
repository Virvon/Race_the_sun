using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class CurrentClickedSpaceshipInfo : MonoBehaviour
    {
        [SerializeField] private CurrentClickedSpacehipWatcher _currentClickedSpaceshipWatcher;
        [SerializeField] private SpaceshipStatPanel[] _spaceshipStatPanels;

        private IPersistentProgressService _persistentProgressService;
        private ModelPoint _modelPoint;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, ModelPoint modelPoint)
        {
            _persistentProgressService = persistentProgressService;
            _modelPoint = modelPoint;
        }

        private void OnEnable() =>
            _currentClickedSpaceshipWatcher.CurrentSpaceshipChanged += OnSpaceshipChanged;

        private void OnDisable() =>
            _currentClickedSpaceshipWatcher.CurrentSpaceshipChanged -= OnSpaceshipChanged;

        private async void OnSpaceshipChanged(SpaceshipType type)
        {
            foreach (SpaceshipStatPanel spaceshipStatPanel in _spaceshipStatPanels)
                spaceshipStatPanel.ResetSpaceship(type);

            await _modelPoint.Change(type);
        }
    }
}