using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.MysteryBox
{
    public class RewardPanel : MonoBehaviour
    {
        private const string ExperienceMessage = "Опыт";

        [SerializeField] private TMP_Text _value;
        [SerializeField] private Button _closeButtone;
        [SerializeField] private GameObject _scoreItemsIcon;
        [SerializeField] private GameObject _experienceIcon;

        private void OnEnable() =>
            _closeButtone.onClick.AddListener(OnCloseButtonClick);

        private void OnDisable() =>
            _closeButtone.onClick.RemoveListener(OnCloseButtonClick);

        public void ShowScoreItemsReward(int reward)
        {
            _experienceIcon.SetActive(false);
            _scoreItemsIcon.SetActive(true);
            _value.text = reward.ToString();
            gameObject.SetActive(true);
        }

        public void ShowExperienveReward()
        {
            _experienceIcon.SetActive(true);
            _scoreItemsIcon.SetActive(false);
            _value.text = ExperienceMessage;
            gameObject.SetActive(true);
        }

        private void OnCloseButtonClick() =>
            gameObject.SetActive(false);
    }
}
