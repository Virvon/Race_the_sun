using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.GameLogic.Trail;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.SpaceshipModelFactory
{
    public interface ISpaceshipModelFactory
    {
        UniTask<SpaceshipModel> CreateSpaceshipModel(SpaceshipType type, Vector3 position, Transform parent = null);
        UniTask<Trail> CreateTrail(TrailType type, Vector3 position, Transform parent);
    }
}