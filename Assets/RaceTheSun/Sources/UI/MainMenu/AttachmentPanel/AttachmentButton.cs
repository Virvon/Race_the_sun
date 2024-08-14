using Assets.RaceTheSun.Sources.Upgrading;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class AttachmentButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UpgradeType _upgradeType;

        public event Action<UpgradeType> Clicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Clicked?.Invoke(_upgradeType);
        }
    }
}