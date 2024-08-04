﻿using Assets.RaceTheSun.Sources.Data;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class CurrentClickedSpacehipWatcher : MonoBehaviour
    {
        [SerializeField] private SpaceshipButton[] _spaceshipButtons;

        private IPersistentProgressService _persistentProgressService;

        public event Action<SpaceshipType> CurrentSpaceshipChanged;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
        }

        private void OnEnable()
        {
            foreach (SpaceshipButton spaceshipButton in _spaceshipButtons)
                spaceshipButton.Clicked += OnSpaceshipButtonClicked;

            _persistentProgressService.Progress.AvailableSpaceships.SpaceshipUnlocked += OnSpaceshipUnlocked;
        }

        private void OnDisable()
        {
            foreach (SpaceshipButton spaceshipInfoButton in _spaceshipButtons)
                spaceshipInfoButton.Clicked -= OnSpaceshipButtonClicked;

            _persistentProgressService.Progress.AvailableSpaceships.SpaceshipUnlocked -= OnSpaceshipUnlocked;
        }

        private void OnSpaceshipButtonClicked(SpaceshipType spaceshipType) =>
            CurrentSpaceshipChanged?.Invoke(spaceshipType);

        private void OnSpaceshipUnlocked(SpaceshipType type) =>
            CurrentSpaceshipChanged?.Invoke(type);
    }
}