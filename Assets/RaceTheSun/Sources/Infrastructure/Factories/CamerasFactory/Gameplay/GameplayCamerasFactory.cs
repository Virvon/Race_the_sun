using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.Gameplay
{
    public class GameplayCamerasFactory : IGameplayCamerasFactory
    {
        private readonly VirtualCamera.Factory _virtualCameraFactory;

        public GameplayCamerasFactory(VirtualCamera.Factory virtualCameraFactory) =>
            _virtualCameraFactory = virtualCameraFactory;

        public SpaceshipMainCamera SpaceshipMainCamera { get; private set; }
        public VirtualCamera SpaceshipSideCamera { get; private set; }
        public VirtualCamera StartCamera { get; private set; }
        public VirtualCamera SpaceshipUpperCamera { get; private set; }
        public VirtualCamera CollisionPortalCamera { get; private set; }
        public VirtualCamera ShieldPortalCamera { get; private set; }

        public async UniTask CreateSpaceshipMainCamera()
        {
            VirtualCamera virtualCamera = await _virtualCameraFactory.Create(GameplayCameraFactoryAssets.SpaceshipMainCamera);
            SpaceshipMainCamera = virtualCamera.GetComponent<SpaceshipMainCamera>();
        }

        public async UniTask CreateSpaceshipSideCamera() =>
            SpaceshipSideCamera = await _virtualCameraFactory.Create(GameplayCameraFactoryAssets.SpaceshipSideCamera);

        public async UniTask CreateStartCamera() =>
            StartCamera = await _virtualCameraFactory.Create(GameplayCameraFactoryAssets.StartCamera);

        public async UniTask CreateSpaceshipUpperCamera() =>
            SpaceshipUpperCamera = await _virtualCameraFactory.Create(GameplayCameraFactoryAssets.SpaceshipUpperCamera);

        public async UniTask CreateCollisionPortalCamera() =>
            CollisionPortalCamera = await _virtualCameraFactory.Create(GameplayCameraFactoryAssets.CollisionPortalCamera);

        public async UniTask CreateShieldCamera() =>
            ShieldPortalCamera = await _virtualCameraFactory.Create(GameplayCameraFactoryAssets.ShieldPortalCamera);
    }
}
