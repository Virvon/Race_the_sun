using Agava.YandexGames;
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
        [SerializeField] private MysteryBoxWindowOpenButton _mysteryBoxWindowOpenButton;

        private IStaticDataService _staticDataService;
        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistentProgressService persistentProgressService)
        {
            _staticDataService = staticDataService;
            _persistentProgressService = persistentProgressService;

            _openMysteryBoxButton.onClick.AddListener(OnOpenMysteryBoxButtonCllicked);

            if (_persistentProgressService.Progress.MysteryBoxes.GetEndDate() <= DateTime.Now)
                _persistentProgressService.Progress.MysteryBoxes.Count = 0;
        }

        private void OnDestroy()
        {
            _openMysteryBoxButton.onClick.RemoveListener(OnOpenMysteryBoxButtonCllicked);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }

        private void OnOpenMysteryBoxButtonCllicked()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            InterstitialAd.Show(onCloseCallback: (_) =>
            {
                MysteryBoxRewardsConfig mysteryBoxRewardsConfig = _staticDataService.GetMysteryBoxRewards();

            int chance = Random.Range(0, 100);

            if (chance <= mysteryBoxRewardsConfig.MinScoreItemsRewardChance)
            {
                int reward = Random.Range(mysteryBoxRewardsConfig.MinScoreItemsRewardMinCount, mysteryBoxRewardsConfig.MinScoreItemsRewardMaxCount);

                _rewardPanel.ShowScoreItemsReward(reward);
                _persistentProgressService.Progress.Wallet.Give(reward);
            }
            else if (chance <= mysteryBoxRewardsConfig.ExperienceRewardChance + mysteryBoxRewardsConfig.MinScoreItemsRewardChance)
            {
                _rewardPanel.ShowExperienveReward();
                _persistentProgressService.Progress.LevelProgress.UpdateExperience(mysteryBoxRewardsConfig.ExperienceReward);
            }
            else
            {
                int reward = Random.Range(mysteryBoxRewardsConfig.MaxScoreItemsRewardMinCount, mysteryBoxRewardsConfig.MaxScoreItemsRewardMaxCount);

                _rewardPanel.ShowScoreItemsReward(reward);
                _persistentProgressService.Progress.Wallet.Give(reward);
            }

            _persistentProgressService.Progress.MysteryBoxes.Take();

            if (_persistentProgressService.Progress.MysteryBoxes.Count <= 0)
            {
                Hide();
                _mysteryBoxWindowOpenButton.gameObject.SetActive(false);
            }
            });
# else
            MysteryBoxRewardsConfig mysteryBoxRewardsConfig = _staticDataService.GetMysteryBoxRewards();

            int chance = Random.Range(0, 100);

            if (chance <= mysteryBoxRewardsConfig.MinScoreItemsRewardChance)
            {
                int reward = Random.Range(mysteryBoxRewardsConfig.MinScoreItemsRewardMinCount, mysteryBoxRewardsConfig.MinScoreItemsRewardMaxCount);

                _rewardPanel.ShowScoreItemsReward(reward);
                _persistentProgressService.Progress.Wallet.Give(reward);
            }
            else if (chance <= mysteryBoxRewardsConfig.ExperienceRewardChance + mysteryBoxRewardsConfig.MinScoreItemsRewardChance)
            {
                _rewardPanel.ShowExperienveReward();
                _persistentProgressService.Progress.LevelProgress.UpdateExperience(mysteryBoxRewardsConfig.ExperienceReward);
            }
            else
            {
                int reward = Random.Range(mysteryBoxRewardsConfig.MaxScoreItemsRewardMinCount, mysteryBoxRewardsConfig.MaxScoreItemsRewardMaxCount);

                _rewardPanel.ShowScoreItemsReward(reward);
                _persistentProgressService.Progress.Wallet.Give(reward);
            }

            _persistentProgressService.Progress.MysteryBoxes.Take();

            if (_persistentProgressService.Progress.MysteryBoxes.Count <= 0)
            {
                Hide();
                _mysteryBoxWindowOpenButton.gameObject.SetActive(false);
            }
#endif
        }
    }
}
