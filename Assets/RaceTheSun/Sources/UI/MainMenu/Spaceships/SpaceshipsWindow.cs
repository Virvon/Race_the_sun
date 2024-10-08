﻿using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class SpaceshipsWindow : OpenableWindow
    {
        [SerializeField] private InformationPanelActivator _informationPanelActivator;

        private MainMenuCameras _mainMenuCameras;

        [Inject]
        private void Construct(MainMenuCameras mainMenuCameras) =>
            _mainMenuCameras = mainMenuCameras;

        public override void Hide()
        {
            gameObject.SetActive(false);
            _informationPanelActivator.Hide();
        }

        public override void Open()
        {
            gameObject.SetActive(true);
            _mainMenuCameras.IncludeCamera(MainMenuCameraType.SelectionCamera);
        }
    }
}