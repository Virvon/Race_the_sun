using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu
{
    public class MainMenuBootstrapper : IInitializable
    {
        private readonly IMainMenuFactory _mainMenuFactory;
        private readonly IUiFactory _uiFactory;

        public MainMenuBootstrapper(IMainMenuFactory mainMenuFactory, IUiFactory uiFactory)
        {
            _mainMenuFactory = mainMenuFactory;
            _uiFactory = uiFactory;
        }

        public async void Initialize()
        {
            await _mainMenuFactory.CreateMainMenu();
            await _mainMenuFactory.CreateEnviroment();
        }
    }
}
