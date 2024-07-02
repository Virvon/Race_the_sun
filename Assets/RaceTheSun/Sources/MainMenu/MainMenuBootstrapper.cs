using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu
{
    public class MainMenuBootstrapper : IInitializable
    {
        private readonly IMainMenuFactory _mainMenuFactory;

        public MainMenuBootstrapper(IMainMenuFactory mainMenuFactory)
        {
            _mainMenuFactory = mainMenuFactory;
        }

        public void Initialize()
        {
            _mainMenuFactory.CreateMainMenu();
        }
    }
}
