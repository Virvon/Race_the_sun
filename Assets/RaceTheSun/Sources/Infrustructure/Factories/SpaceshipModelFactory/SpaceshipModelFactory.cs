using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.GameLogic.Trail;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Infrustructure.Factories.SpaceshipModelFactory
{
    public class SpaceshipModelFactory : ISpaceshipModelFactory
    {
        private readonly SpaceshipModel.Factory _spaceshipModelFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly Trail.Factory _trailFactory;

        public SpaceshipModelFactory(SpaceshipModel.Factory spaceshipModelFactory, IStaticDataService staticDataService, Trail.Factory trailFactory)
        {
            _spaceshipModelFactory = spaceshipModelFactory;
            _staticDataService = staticDataService;
            _trailFactory = trailFactory;
        }

        public async UniTask<SpaceshipModel> CreateSpaceshipModel(SpaceshipType type, Vector3 position, Transform parent = null)
        {
            SpaceshipModel spaceshipModel = await _spaceshipModelFactory.Create(_staticDataService.GetSpaceship(type).ModelPrefabReference);

            spaceshipModel.transform.parent = parent;
            spaceshipModel.transform.position = position;

            return spaceshipModel;
        }

        public async UniTask<Trail> CreateTrail(TrailType type, Vector3 position, Transform parent)
        {
            Trail trail = await _trailFactory.Create(_staticDataService.GetTrail(type).Reference);

            trail.transform.parent = parent;
            trail.transform.position = position;

            return trail;
        }
    }
}