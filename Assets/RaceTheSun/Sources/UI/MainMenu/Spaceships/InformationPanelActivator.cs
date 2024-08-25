using UnityEngine;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class InformationPanelActivator : MonoBehaviour
    {
        [SerializeField] private InformationButton[] _informationButtons;

        private InformationButton _currentInformationButton;

        private void OnEnable()
        {
            foreach (InformationButton infoButton in _informationButtons)
                infoButton.ShowedInformation += OnInfoButtonShowedInformation;
        }

        private void OnDisable()
        {
            foreach (InformationButton infoButton in _informationButtons)
                infoButton.ShowedInformation -= OnInfoButtonShowedInformation;
        }

        public void Hide()
        {
            _currentInformationButton?.HideInfo();
        }

        private void OnInfoButtonShowedInformation(InformationButton informationButton)
        {
            _currentInformationButton?.HideInfo();
            _currentInformationButton = informationButton;
            _currentInformationButton.OpenInfo();
        }
    }
}