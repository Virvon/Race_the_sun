using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.MysteryBox
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

            _button.onClick.AddListener(OnButtonClick);

            if (_persistentProgressService.Progress.MysteryBoxes.GetEndDate() <= DateTime.Now || persistentProgressService.Progress.MysteryBoxes.Count == 0)
                gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _mysteryBoxWindow.Open();
        }
    }
}
