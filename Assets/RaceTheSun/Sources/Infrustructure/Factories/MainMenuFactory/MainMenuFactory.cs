using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class MainMenuFactory : IMainMenuFactory
    {
        private readonly UI.MainMenu.MainMenu.Factory _mainMenuFactory;

        public MainMenuFactory(UI.MainMenu.MainMenu.Factory mainMenuFactory)
        {
            _mainMenuFactory = mainMenuFactory;
        }

        public UniTask CreateEnviroment()
        {
            return default;
        }

        public async UniTask CreateMainMenu() =>
            await _mainMenuFactory.Create(MainMenuFactoryAssets.MainMenu);
    }
}