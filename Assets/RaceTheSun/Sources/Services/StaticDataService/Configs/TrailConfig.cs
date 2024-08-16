using Assets.RaceTheSun.Sources.Trail;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [CreateAssetMenu(fileName = "TrailConfig", menuName = "StaticData/Create new trail config", order = 51)]
    public class TrailConfig : ScriptableObject
    {
        public TrailType Type;
        public AssetReferenceGameObject Reference;
        public int BuyCost;
        public bool IsUnlockedOnStart;
        public string Name;
        public string Title;
    }
}
