using Assets.RaceTheSun.Sources.GameLogic.Audio;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;
using Assets.RaceTheSun.Sources.Gameplay.Bird;
using Assets.RaceTheSun.Sources.Gameplay.CollectItems;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Collision;
using Assets.RaceTheSun.Sources.Gameplay.Sun;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.Infrustructure.AssetManagement;
using Assets.RaceTheSun.Sources.UI.GameOverPanel;
using Assets.RaceTheSun.Sources.UI.Hud;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrustructure.Factories.GameplayFactory
{
    public class GameplayFactoryInstaller : Installer<GameplayFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameplayFactory>().To<GameplayFactory>().AsSingle();

            Container
                .BindFactory<string, UniTask<Hud>, Hud.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<Hud>>();

            Container
                .BindFactory<string, UniTask<Spaceship>, Spaceship.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<Spaceship>>();

            Container
                .BindFactory<AssetReferenceGameObject, UniTask<Tile>, Tile.Factory>()
                .FromFactory<RefefencePrefabFactoryAsync<Tile>>();

            Container
                .BindFactory<string, UniTask<WorldGenerator>, WorldGenerator.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<WorldGenerator>>();

            Container
                .BindFactory<string, UniTask<VirtualCamera>, VirtualCamera.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<VirtualCamera>>();

            Container
                .BindFactory<string, UniTask<Sun>, Sun.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<Sun>>();

            Container
                .BindFactory<string, UniTask<SpaceshipShieldPortal>, SpaceshipShieldPortal.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<SpaceshipShieldPortal>>();

            Container
                .BindFactory<string, UniTask<GameOverPanel>, GameOverPanel.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<GameOverPanel>>();

            Container
                .BindFactory<string, UniTask<JumpBoost>, JumpBoost.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<JumpBoost>>();

            Container
                .BindFactory<string, UniTask<Shield>, Shield.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<Shield>>();

            Container
                .BindFactory<string, UniTask<ShieldPortal>, ShieldPortal.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<ShieldPortal>>();

            Container
                .BindFactory<string, UniTask<Bird>, Bird.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<Bird>>();

            Container
                .BindFactory<string, UniTask<ScoreItem>, ScoreItem.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<ScoreItem>>();

            Container
                .BindFactory<string, UniTask<SpeedBoost>, SpeedBoost.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<SpeedBoost>>();

            Container
                .BindFactory<string, UniTask<StageMusic>, StageMusic.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<StageMusic>>();

            Container
                .BindFactory<string, UniTask<Gameplay.Spaceship.Plane>, Gameplay.Spaceship.Plane.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<Gameplay.Spaceship.Plane>>();

            Container
                .BindFactory<string, UniTask<CollectItemsSoundEffects>, CollectItemsSoundEffects.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<CollectItemsSoundEffects>>();

            Container
                .BindFactory<string, UniTask<PortalSound>, PortalSound.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<PortalSound>>();

            Container
                .BindFactory<string, UniTask<DestroySound>, DestroySound.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<DestroySound>>();

            Container
                .BindFactory<string, UniTask<CollisionFx>, CollisionFx.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<CollisionFx>>();
        }
    }
}
