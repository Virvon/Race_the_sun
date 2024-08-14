using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Trail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private IPersistentProgressService _persistentProgressService;
        private IStaticDataService _staticDataService;
        private bool _isHided;

        public event Action Clicked;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;

            Hide();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void Hide()
        {
            _isHided = true;
            gameObject.SetActive(false);
        }

        public void ShowInfo(TrailType trailType)
        {
            if (_isHided)
                Open();

            if(_persistentProgressService.Progress.AvailableTrails.IsUnlocked(trailType))
            {
                _button.interactable = _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType).TrailType != trailType;
                _buttonText.text = _unlockedButtonText;
            }
            else
            {
                int trailCost = _staticDataService.GetTrail(trailType).BuyCost;

                _button.interactable = trailCost <= _persistentProgressService.Progress.Wallet.Value;
                _buttonText.text = trailCost.ToString();
            }
        }

        private void OnButtonClicked()
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
