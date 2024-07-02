using Assets.MyBakery.Sources.Services.StaticData;
using Cysharp.Threading.Tasks;

namespace Assets.MyBakery.Sources.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        LevelStaticData GetLevelStaticData(string sceneName);
    }
}