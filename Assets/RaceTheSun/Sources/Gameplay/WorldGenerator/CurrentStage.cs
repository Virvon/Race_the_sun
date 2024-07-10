using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class CurrentStage
    {
        private const int StagesCount = 1;

        private int _currentStage;
        private bool _ifStageFinished;

        public CurrentStage()
        {
            _currentStage = 0;
            _ifStageFinished = false;
        }

        public int GetCurrentStage()
        {
            int currentStage = _currentStage;

            if(currentStage == 0)
            {
                _currentStage++;
                return (int)Stage.StartStage;
            }

            if(_ifStageFinished == false)
            {
                _ifStageFinished = true;
                return Cheack(currentStage + 1);
            }
            else
            {
                _currentStage++;
                _ifStageFinished = false;
                return (int)Stage.BetweenStages;
            }
        }

        private int Cheack(int targetStage) =>
            targetStage > StagesCount ? 1 : targetStage;
    }
}
