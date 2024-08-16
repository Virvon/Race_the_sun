using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class SpaceshipButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SpaceshipType _spaceshipType;
        [SerializeField] private CurrentClickedSpacehipWatcher _currentClickedSpacehipWatcher;
        [SerializeField] private GameObject _selectFrame;
        [SerializeField] private GameObject _blockPanel;
        [SerializeField] private GameObject _useIcon;

        private IPersistentProgressService _persistentProgressService;

        public event Action<SpaceshipType> Clicked;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
            _currentClickedSpacehipWatcher.CurrentSpaceshipChanged += OnCurrentSpaceshipChanged;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
            _currentClickedSpacehipWatcher.CurrentSpaceshipChanged -= OnCurrentSpaceshipChanged;

        }

        private void OnCurrentSpaceshipChanged(SpaceshipType spaceshipType)
        {
            _selectFrame.SetActive(spaceshipType == _spaceshipType);
            _blockPanel.SetActive(_persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_spaceshipType).IsUnlocked == false);
            _useIcon.SetActive(_persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType == _spaceshipType);
        }

        private void OnClicked()
        {
            Clicked?.Invoke(_spaceshipType);
        }
    }
}