using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        UniTask Load(string scene);
    }
}