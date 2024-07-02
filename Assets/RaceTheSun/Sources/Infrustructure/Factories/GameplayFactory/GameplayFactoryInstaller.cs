using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory
{
    public class GameplayFactoryInstaller : Installer<GameplayFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameplayFactory>().To<GameplayFactory>().AsSingle();

            Container
                .BindFactory<string, UniTask<GameObject>, HudFactory>()
                .FromFactory<KeyPrefabFactoryAsync<GameObject>>();

            Container
                .BindFactory<string, UniTask<Spaceship>, Spaceship.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<Spaceship>>();
        }
    }
}
