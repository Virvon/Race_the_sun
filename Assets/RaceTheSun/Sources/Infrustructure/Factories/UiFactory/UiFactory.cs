using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UiFactory : IUiFactory
{
    private readonly IStaticDataService _staticData;
    private readonly UiRoot.Factory _uiRootFactory;

    private Transform _uiRoot;

    public UiFactory(IStaticDataService staticData, UiRoot.Factory uiRootFactory)
    {
        _staticData = staticData;
        _uiRootFactory = uiRootFactory;
    }

    public async UniTask CreateUiRoot()
    {
        UiRoot uiRoot = await _uiRootFactory.Create(UiFactoryAssets.UiRoot);
        _uiRoot = uiRoot.transform;
    }

    public async UniTask CreateShop()
    {

    }
}