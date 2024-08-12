using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class SpaceshipModelFactoryInstaller : Installer<SpaceshipModelFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISpaceshipModelFactory>().To<SpaceshipModelFactory>().AsSingle();

            Container
               .BindFactory<AssetReferenceGameObject, UniTask<SpaceshipModel>, SpaceshipModel.Factory>()
               .FromFactory<RefefencePrefabFactoryAsync<SpaceshipModel>>();
        }
    }
}