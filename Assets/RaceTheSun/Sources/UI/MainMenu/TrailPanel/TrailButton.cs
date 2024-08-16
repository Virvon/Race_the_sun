using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Trail;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.TrailPanel
{
    public class TrailButton : MonoBehaviour
    {
        [SerializeField] private TrailType _trailType;
        [SerializeField] private Button _button;
        [SerializeField] private TrailInfoPanel _trailInfoPanel;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;
        [SerializeField] private GameObject _lockIcon;
        [SerializeField] private GameObject _useIcon;
        [SerializeField] private GameObject _selectFrame;
        [SerializeField] private TMP_Text _name;

        private IPersistentProgressService _persistentProgressService;

        public event Action<TrailType, GameObject> Clicked;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;

            _name.text = staticDataService.GetTrail(_trailType).Name;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
            _trailInfoPanel.Clicked += ChangeInfo;
            ChangeInfo();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
            _trailInfoPanel.Clicked -= ChangeInfo;
        }

        private void OnButtonClick()
        {
            Clicked?.Invoke(_trailType, _selectFrame);
        }

        private void ChangeInfo()
        {
            _lockIcon.SetActive(_persistentProgressService.Progress.AvailableTrails.IsUnlocked(_trailType) == false);
            _useIcon.SetActive(_persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType).TrailType == _trailType);
        }
    }
}
