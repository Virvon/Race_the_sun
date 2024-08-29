using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships;
using Assets.RaceTheSun.Sources.Upgrading;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.AttachmentPanel
{
    public class AttachmentButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private AttachmentInfoPanel _attachmentInfoPanel;
        [SerializeField] private GameObject _useIcon;
        [SerializeField] private GameObject _blockIcon;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;
        [SerializeField] private GameObject _selectFrame;
        [SerializeField] private TMP_Text _name;

        private IPersistentProgressService _persistentProgressService;

        public event Action<UpgradeType, GameObject> Clicked;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;

            _blockIcon.SetActive(_persistentProgressService.Progress.Upgrading.IsUpgraded(_upgradeType) == false);
            _name.text = staticDataService.GetAttachment(_upgradeType).Name;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
            _attachmentInfoPanel.Clicked += ChangeInfo;

            ChangeInfo();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
            _attachmentInfoPanel.Clicked -= ChangeInfo;
        }

        private void ChangeInfo() =>
            _useIcon.SetActive(_persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType).UpgradeTypes.Contains(_upgradeType));

        private void OnClicked() =>
            Clicked?.Invoke(_upgradeType, _selectFrame);
    }
}