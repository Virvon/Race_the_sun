using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.Gameplay
{
    public interface IGameplayCamerasFactory
    {
        VirtualCamera CollisionPortalCamera { get; }
        SpaceshipMainCamera SpaceshipMainCamera { get; }
        VirtualCamera SpaceshipSideCamera { get; }
        VirtualCamera SpaceshipUpperCamera { get; }
        VirtualCamera StartCamera { get; }
        VirtualCamera ShieldPortalCamera { get; }

        UniTask CreateCollisionPortalCamera();
        UniTask CreateShieldCamera();
        UniTask CreateSpaceshipMainCamera();
        UniTask CreateSpaceshipSideCamera();
        UniTask CreateSpaceshipUpperCamera();
        UniTask CreateStartCamera();
    }
}