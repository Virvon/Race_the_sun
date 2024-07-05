using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class UiFactoryInstaller : Installer<UiFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IUiFactory>().To<UiFactory>().AsSingle();

            Container
                .BindFactory<string, UniTask<UiRoot>, UiRoot.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<UiRoot>>();
        }
    }
}