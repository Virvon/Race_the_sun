using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Assets.RaceTheSun.Sources.UI.MainMenu;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.RaceTheSun.Sources.UI.MysteryBox
{
    public partial class MysteryBoxWindow : OpenableWindow
    {
        [SerializeField] private Button _openMysteryBoxButton;
        [SerializeField] private RewardPanel _rewardPanel;

        private IStaticDataService _staticDataService;
        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistentProgressService persistentProgressService)
        {
            _staticDataService = staticDataService;
            _persistentProgressService = persistentProgressService;

            if (_persistentProgressService.Progress.MysteryBoxes.GetEndDate() <= DateTime.Now)
                _persistentProgressService.Progress.MysteryBoxes.Count = 0;

            _openMysteryBoxButton.onClick.AddListener(OnOpenMysteryBoxButtonCllicked);
            _persistentProgressService.Progress.MysteryBoxes.CountChanged += OnMysteryBoxCountChanged;
        }

        private void OnDestroy()
        {
            _openMysteryBoxButton.onClick.RemoveListener(OnOpenMysteryBoxButtonCllicked);
            _persistentProgressService.Progress.MysteryBoxes.CountChanged -= OnMysteryBoxCountChanged;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }

        private void OnMysteryBoxCountChanged(int count)
        {
            if (count <= 0)
            {
                Hide();
            }
        }

        private void OnOpenMysteryBoxButtonCllicked()
        {    
            MysteryBoxRewardsConfig mysteryBoxRewardsConfig = _staticDataService.GetMysteryBoxRewards();

            int chance = Random.Range(0, 100);

            if (chance <= mysteryBoxRewardsConfig.MinScoreItemsRewardChance)
            {
                int reward = Random.Range(mysteryBoxRewardsConfig.MinScoreItemsRewardMinCount, mysteryBoxRewardsConfig.MinScoreItemsRewardMaxCount);

                _rewardPanel.ShowScoreItemsReward(reward);
                _persistentProgressService.Progress.Wallet.Take(reward);
            }
            else if (chance <= mysteryBoxRewardsConfig.ExperienceRewardChance + mysteryBoxRewardsConfig.MinScoreItemsRewardChance)
            {
                Debug.Log("experience");
            }
            else
            {
                int reward = Random.Range(mysteryBoxRewardsConfig.MaxScoreItemsRewardMinCount, mysteryBoxRewardsConfig.MaxScoreItemsRewardMaxCount);

                _rewardPanel.ShowScoreItemsReward(reward);
                _persistentProgressService.Progress.Wallet.Take(reward);
            }

            _persistentProgressService.Progress.MysteryBoxes.Take();
        }
    }
}
