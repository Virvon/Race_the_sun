using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.Services.SaveLoad;
using Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships;
using Assets.RaceTheSun.Sources.Upgrading;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.AttachmentPanel
{
    public class AttachmentWindow : OpenableWindow
    {
        [SerializeField] private AttachmentButton[] _attachmentButtons;
        [SerializeField] private AttachmentInfoPanel _attachmentInfoPanel;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;
        [SerializeField] private AttachmentCell[] _attachmentCells;

        private UpgradeType _currentUpgradeType;
        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;
        private GameObject _currentSelectFrame;


        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;

            foreach (AttachmentButton attachmentButton in _attachmentButtons)
                attachmentButton.Clicked += OnAttachmentButtonClicked;

            _attachmentInfoPanel.Clicked += OnAttachmentInfoPanelClicked;
        }

        private void OnDestroy()
        {
            foreach (AttachmentButton attachmentButton in _attachmentButtons)
                attachmentButton.Clicked -= OnAttachmentButtonClicked;

            _attachmentInfoPanel.Clicked -= OnAttachmentInfoPanelClicked;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            _currentSelectFrame?.SetActive(false);
            _attachmentInfoPanel.Hide();
        }

        public override void Open()
        {
            gameObject.SetActive(true);

            foreach (AttachmentCell attachmentCell in _attachmentCells)
                attachmentCell.Reset();

            List<UpgradeType> upgradedTypes = _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType).UpgradeTypes;

            for (int i = 0; i < _attachmentCells.Length && i < upgradedTypes.Count; i++)
                _attachmentCells[i].TryUse(upgradedTypes[i], _currentClickedSpaceshipInfo.SpaceshipType);
        }

        private void OnAttachmentButtonClicked(UpgradeType upgradeType, GameObject selectFrame)
        {
            _currentUpgradeType = upgradeType;

            _currentSelectFrame?.SetActive(false);
            _currentSelectFrame = selectFrame;
            _currentSelectFrame.SetActive(true);

            _attachmentInfoPanel.ShowInfo(_currentUpgradeType);
        }

        private void OnAttachmentInfoPanelClicked()
        {
            if (_persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType).UpgradeTypes.Contains(_currentUpgradeType))
            {
                foreach (AttachmentCell attachmentCell in _attachmentCells)
                {
                    if (attachmentCell.TryRemove(_currentUpgradeType, _currentClickedSpaceshipInfo.SpaceshipType))
                    {
                        break;
                    }
                }
            }
            else
            {
                foreach (AttachmentCell attachmentCell in _attachmentCells)
                {
                    if (attachmentCell.TryUse(_currentUpgradeType, _currentClickedSpaceshipInfo.SpaceshipType))
                    {
                        break;
                    }
                }
            }

            _saveLoadService.SaveProgress();
            _currentSelectFrame.SetActive(false);
            _attachmentInfoPanel.Hide();
        }
    }
}