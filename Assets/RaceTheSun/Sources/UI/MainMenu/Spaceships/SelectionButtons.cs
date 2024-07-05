using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class SelectionButtons : MonoBehaviour
    {
        [SerializeField] private SpaceshipSelection _spaceshipSelection;
        [SerializeField] private Button _buyButton;
        [SerializeField] private GameObject _selectOrCustomizePanel;
        [SerializeField] private Button _selectButton;

        private void OnEnable()
        {
            _spaceshipSelection.SpaceshipSelected += OnSpaceshipSelected;
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _selectButton.onClick.AddListener(OnSelcectButtonClicked);
        }

        private void OnDisable()
        {
            _spaceshipSelection.SpaceshipSelected -= OnSpaceshipSelected;
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
            _selectButton.onClick.RemoveListener(OnSelcectButtonClicked);
        }

        private void OnBuyButtonClicked()
        {
            _spaceshipSelection.Unlock();
        }

        private void OnSelcectButtonClicked()
        {
            _spaceshipSelection.Choose();
        }

        private void OnSpaceshipSelected(SpaceshipInfo spaceshipInfo)
        {
            if(spaceshipInfo.IsUnlocked)
            {
                _buyButton.gameObject.SetActive(false);
                _selectOrCustomizePanel.SetActive(true);
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _selectOrCustomizePanel.SetActive(false);
                _buyButton.interactable = _spaceshipSelection.CanBuyCurrentShip;
            }
        }
    }
}