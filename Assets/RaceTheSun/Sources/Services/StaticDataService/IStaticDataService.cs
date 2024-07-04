using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        WindowConfig GetWindow(WindowId windowId);
    }
}