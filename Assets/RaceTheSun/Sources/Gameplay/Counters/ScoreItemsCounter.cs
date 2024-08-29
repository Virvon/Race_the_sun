using Assets.RaceTheSun.Sources.Services.PersistentProgress;

namespace Assets.RaceTheSun.Sources.Gameplay.Counters
{
    public class ScoreItemsCounter
    {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly MultiplierProgressCounter _progressMultiplierCounter;

        public ScoreItemsCounter(IPersistentProgressService persistentProgressService, MultiplierProgressCounter progressMultiplierCounter)
        {
            _persistentProgressService = persistentProgressService;

            ScoreItemsPerGame = 0;
            _progressMultiplierCounter = progressMultiplierCounter;
        }

        public int ScoreItemsPerGame { get; private set; }

        public void Give()
        {
            ScoreItemsPerGame++;
            _persistentProgressService.Progress.Wallet.Give(1);
            _progressMultiplierCounter.GiveScoreItem();
        }
    }
}
