using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.MysteryBox
{
    public class MysteryBoxesCountPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countValue;

        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

            _persistentProgressService.Progress.MysteryBoxes.CountChanged += ChangeCountValue;

            ChangeCountValue(_persistentProgressService.Progress.MysteryBoxes.Count);
        }

        private void OnDestroy() =>
            _persistentProgressService.Progress.MysteryBoxes.CountChanged -= ChangeCountValue;

        private void ChangeCountValue(int count) =>
            _countValue.text = count.ToString();
    }
}
