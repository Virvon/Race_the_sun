using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class MainMenuFactory : IMainMenuFactory
    {
        private readonly HudFactory _hudFactory;

        public MainMenuFactory(HudFactory hudFactory)
        {
            _hudFactory = hudFactory;
        }

        public async UniTask CreateMainMenu() =>
            await _hudFactory.Create(MainMenuFactoryAssets.MainMenu);
    }
}