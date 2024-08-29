using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.Gameplay
{
    public class GameplayCamerasFactoryInstaller : Installer<GameplayCamerasFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IGameplayCamerasFactory>()
                .To<GameplayCamerasFactory>()
                .AsSingle();

            Container
                .BindFactory<string, UniTask<VirtualCamera>, VirtualCamera.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<VirtualCamera>>();
        }
    }
}