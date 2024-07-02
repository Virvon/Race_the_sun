using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public interface IMainMenuFactory
    {
        UniTask CreateMainMenu();
    }
}