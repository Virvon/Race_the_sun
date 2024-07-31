using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public interface IMainMenuFactory
    {
        UniTask CreateMainMenu();
        UniTask CreateEnviroment();
    }
}