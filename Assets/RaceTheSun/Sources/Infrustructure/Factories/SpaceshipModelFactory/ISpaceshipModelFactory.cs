using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Trail;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public interface ISpaceshipModelFactory
    {
        UniTask<SpaceshipModel> CreateSpaceshipModel(SpaceshipType type, Vector3 position, Transform parent = null);
        UniTask<Trail.Trail> CreateTrail(TrailType type, Vector3 position, Transform parent);
    }
}