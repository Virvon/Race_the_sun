using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Upgrading;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class AttachmentInfoPanel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private string _removeText;
        [SerializeField] private string _equipText;

        private IPersistentProgressService _persistentProgressService;
        private bool _isHided;

        public event Action Clicked;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

            _button.onClick.AddListener(OnButtonClick);

            Hide();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void Hide()
        {
            _isHided = true;
            gameObject.SetActive(false);
        }

        public void ShowInfo(UpgradeType upgradeType)
        {
            if (_isHided)
                Open();

            if (_persistentProgressService.Progress.Upgrading.IsUpgraded(upgradeType))
            {
                SpaceshipData spaceshipData = _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType);

                if (spaceshipData.UpgradeTypes.Contains(upgradeType))
                {
                    _button.interactable = true;
                    _buttonText.text = _removeText;
                }
                else
                {
                    _button.interactable = spaceshipData.UpgradeTypes.Count < _persistentProgressService.Progress.Upgrading.AttachmentCellsCount;
                    _buttonText.text = _equipText;
                }
            }
            else
            {
                _button.interactable = false;
                _buttonText.text = "";
            }
        }

        private void OnButtonClick()
        {
            Clicked?.Invoke();
        }

        private void Open()
        {
            _isHided = false;
            gameObject.SetActive(true);
        }
    }
}