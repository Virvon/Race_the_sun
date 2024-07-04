using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.UI;
using Assets.RaceTheSun.Sources.UI.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UiFactory : IUiFactory
{
    private readonly IStaticDataService _staticData;
    private readonly UiRoot.Factory _uiRootFactory;
    private readonly Window.Factory _windowFactory;

    private Transform _uiRoot;

    public UiFactory(IStaticDataService staticData, UiRoot.Factory uiRootFactory, Window.Factory windowFactory)
    {
        _staticData = staticData;
        _uiRootFactory = uiRootFactory;
        _windowFactory = windowFactory;
    }

    public async UniTask CreateUiRoot()
    {
        UiRoot uiRoot = await _uiRootFactory.Create(UiFactoryAssets.UiRoot);
        _uiRoot = uiRoot.transform;
    }

    public async UniTask CreateShop()
    {
        WindowConfig config = _staticData.GetWindow(WindowId.Shop);
        Window window = await _windowFactory.Create(config.Prefabreference);
        window.transform.SetParent(_uiRoot);
    }
}