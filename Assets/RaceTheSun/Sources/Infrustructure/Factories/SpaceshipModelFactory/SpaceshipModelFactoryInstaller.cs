using Assets.RaceTheSun.Sources.Infrustructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrustructure.Factories.SpaceshipModelFactory
{
    public class SpaceshipModelFactoryInstaller : Installer<SpaceshipModelFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISpaceshipModelFactory>().To<SpaceshipModelFactory>().AsSingle();

            Container
               .BindFactory<AssetReferenceGameObject, UniTask<SpaceshipModel>, SpaceshipModel.Factory>()
               .FromFactory<RefefencePrefabFactoryAsync<SpaceshipModel>>();

            Container
               .BindFactory<AssetReferenceGameObject, UniTask<GameLogic.Trail.Trail>, GameLogic.Trail.Trail.Factory>()
               .FromFactory<RefefencePrefabFactoryAsync<GameLogic.Trail.Trail>>();
        }
    }
}