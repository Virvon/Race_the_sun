using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public abstract class InformationButton : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _infoPanel;
        [SerializeField] private TMP_Text _info;

        public event Action<InformationButton> ShowedInformation;

        protected TMP_Text Info => _info;

        protected virtual void OnEnable()
        {
            _closeButton.onClick.AddListener(HideInfo);
            _openButton.onClick.AddListener(OnOpenButtonClilcked);
        }

        protected virtual void OnDisable()
        {
            _closeButton.onClick.RemoveListener(HideInfo);
            _openButton.onClick.RemoveListener(OnOpenButtonClilcked);
        }

        public virtual void OpenInfo()
        {
            _infoPanel.SetActive(true);
        }

        public void HideInfo()
        {
            _infoPanel.SetActive(false);
        }

        private void OnOpenButtonClilcked()
        {
            ShowedInformation?.Invoke(this);
        }
    }
}