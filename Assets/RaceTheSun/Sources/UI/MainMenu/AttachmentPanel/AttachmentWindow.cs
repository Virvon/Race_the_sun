using Assets.RaceTheSun.Sources.Upgrading;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class AttachmentWindow : OpenableWindow
    {
        [SerializeField] private AttachmentButton[] _attachmentButtons;
        [SerializeField] private AttachmentInfoPanel _attachmentInfoPanel;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;

        private UpgradeType _currentUpgradeType;
        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

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
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }

        private void OnAttachmentButtonClicked(UpgradeType upgradeType)
        {
            _currentUpgradeType = upgradeType;

            _attachmentInfoPanel.ShowInfo(_currentUpgradeType);
        }

        private void OnAttachmentInfoPanelClicked()
        {
            if (_persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType).UpgradeTypes.Contains(_currentUpgradeType))
            {
                _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType).UpgradeTypes.Remove(_currentUpgradeType);
            }
            else
            {
                _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType).UpgradeTypes.Add(_currentUpgradeType);
            }

            _attachmentInfoPanel.Hide();
        }
    }
}