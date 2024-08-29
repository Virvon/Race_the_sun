using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class Education : MonoBehaviour
    {
        [SerializeField] private GameObject _educationInfo;
        [SerializeField] private Button _closeEducationButton;

        private void OnEnable() =>
            _closeEducationButton.onClick.AddListener(CloseEducation);

        private void OnDisable() =>
            _closeEducationButton.onClick.RemoveListener(CloseEducation);

        public void ShowEducation() =>
            _educationInfo.SetActive(true);

        private void CloseEducation() =>
            _educationInfo.SetActive(false);
    }
}