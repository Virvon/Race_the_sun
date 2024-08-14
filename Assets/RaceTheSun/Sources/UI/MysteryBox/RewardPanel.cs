using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MysteryBox
{
    public class RewardPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Button _closeButtone;

        private void OnEnable()
        {
            _closeButtone.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDisable()
        {
            _closeButtone.onClick.RemoveListener(OnCloseButtonClick);
        }

        public void ShowScoreItemsReward(int reward)
        {
            _value.text = reward.ToString();
            gameObject.SetActive(true);
        }

        private void OnCloseButtonClick()
        {
            gameObject.SetActive(false);
        }
    }
}
