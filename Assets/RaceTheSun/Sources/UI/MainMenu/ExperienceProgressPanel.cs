using Assets.RaceTheSun.Sources.Data;
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

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _levelValue.text = persistentProgressService.Progress.LevelProgress.Level.ToString();

            if (persistentProgressService.Progress.LevelProgress.IsMaxLevel)
            {
                _progressbar.fillAmount = 1;
                _maxLevelText.enabled = true;
            }
            else
            {
                _progressbar.fillAmount = persistentProgressService.Progress.LevelProgress.Experience / LevelProgress.ExperienceToLevelUp;
            }
            
        }
    }
}
