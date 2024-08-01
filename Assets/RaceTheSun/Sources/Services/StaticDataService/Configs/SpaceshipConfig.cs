using Assets.RaceTheSun.Sources.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [CreateAssetMenu(fileName = "SpaceshipConfig", menuName = "StaticData/Create new spaceship config", order = 51)]
    public class SpaceshipConfig : ScriptableObject
    {
        public SpaceshipType Type;
        public AssetReferenceGameObject ModelPrefabReference;
    }
}
