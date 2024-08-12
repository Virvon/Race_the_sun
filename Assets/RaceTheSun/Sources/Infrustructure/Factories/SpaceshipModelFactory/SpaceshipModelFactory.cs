using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class SpaceshipModelFactory : ISpaceshipModelFactory
    {
        private readonly SpaceshipModel.Factory _spaceshipModelFactory;
        private readonly IStaticDataService _staticDataService;

        public SpaceshipModelFactory(SpaceshipModel.Factory spaceshipModelFactory, IStaticDataService staticDataService)
        {
            _spaceshipModelFactory = spaceshipModelFactory;
            _staticDataService = staticDataService;
        }

        public async UniTask<SpaceshipModel> CreateSpaceshipModel(SpaceshipType type, Vector3 position, Transform parent = null)
        {
            SpaceshipModel spaceshipModel = await _spaceshipModelFactory.Create(_staticDataService.GetSpaceship(type).ModelPrefabReference);

            spaceshipModel.transform.parent = parent;
            spaceshipModel.transform.position = position;

            return spaceshipModel;
        }
    }
}