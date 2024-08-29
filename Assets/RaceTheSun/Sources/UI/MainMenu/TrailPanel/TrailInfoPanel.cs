using System;
using Assets.RaceTheSun.Sources.GameLogic.Trail;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.TrailPanel
{
    public class TrailInfoPanel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _unlockedButtonText;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private TMP_Text _trailName;
        [SerializeField] private TMP_Text _trailTitle;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;
        [SerializeField] private GameObject _buyIcon;

        private IPersistentProgressService _persistentProgressService;
        private IStaticDataService _staticDataService;
        private bool _isHided;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;

            Hide();
        }

        public event Action Clicked;

        public bool IsLocked { get; private set; }
        public bool IsUsed { get; private set; }

        private void OnEnable() =>
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnButtonClicked);

        public void Hide()
        {
            _isHided = true;
            gameObject.SetActive(false);
        }

        public void ShowInfo(TrailType trailType)
        {
            if (_isHided)
                Open();

            if (_persistentProgressService.Progress.AvailableTrails.IsUnlocked(trailType))
            {
                _button.interactable = _persistentProgressService
                    .Progress
                    .AvailableSpaceships
                    .GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType)
                    .TrailType != trailType;

                _buttonText.text = _unlockedButtonText;
                _buyIcon.SetActive(false);
            }
            else
            {
                int trailCost = _staticDataService.GetTrail(trailType).BuyCost;

                _button.interactable = trailCost <= _persistentProgressService.Progress.Wallet.Value;
                _buttonText.text = trailCost.ToString();
                _buyIcon.SetActive(true);
            }

            _trailName.text = _staticDataService.GetTrail(trailType).Name;
            _trailTitle.text = _staticDataService.GetTrail(trailType).Title;
        }

        private void OnButtonClicked() =>
            Clicked?.Invoke();

        private void Open()
        {
            _isHided = false;
            gameObject.SetActive(true);
        }
    }
}
