using Assets.RaceTheSun.Sources.Data;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class ExperienceProgressPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelValue;
        [SerializeField] private MPUIKIT.MPImage _progressbar;
        [SerializeField] private TMP_Text _maxLevelText;

        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
        }

        private void OnEnable()
        {
            ChangeInfo();

            _persistentProgressService.Progress.LevelProgress.ExperienceCountChanged += ChangeInfo;
        }

        private void OnDisable()
        {
            _persistentProgressService.Progress.LevelProgress.ExperienceCountChanged -= ChangeInfo;
        }

        private void ChangeInfo()
        {
            _levelValue.text = _persistentProgressService.Progress.LevelProgress.Level.ToString();

            if (_persistentProgressService.Progress.LevelProgress.IsMaxLevel)
            {
                _progressbar.fillAmount = 1;
                _maxLevelText.gameObject.SetActive(true);
            }
            else
            {
                _progressbar.fillAmount = (float)_persistentProgressService.Progress.LevelProgress.Experience / LevelProgress.ExperienceToLevelUp;
            }
        }
    }
}
