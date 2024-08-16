using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MysteryBox
{
    public class MysteryBoxWindowOpenButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MysteryBoxWindow _mysteryBoxWindow;

        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

            if (_persistentProgressService.Progress.MysteryBoxes.GetEndDate() <= DateTime.Now || persistentProgressService.Progress.MysteryBoxes.Count == 0)
                gameObject.SetActive(false);

            _button.onClick.AddListener(OnButtonClick);
            _persistentProgressService.Progress.MysteryBoxes.CountChanged += OnMysteryBoxesCountChanged;
        }

        private void OnDestroy()
        {
            _persistentProgressService.Progress.MysteryBoxes.CountChanged -= OnMysteryBoxesCountChanged;
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnMysteryBoxesCountChanged(int count)
        {
            if (count <= 0)
                gameObject.SetActive(false);
        }

        private void OnButtonClick()
        {
            _mysteryBoxWindow.Open();
        }
    }
}
