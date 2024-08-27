using System;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [Serializable]
    public class LevelUnclockInfoConfig
    {
        public int Level;
        public AssetReference IconReference;
        public string Title;
        public string Subtitle;
        public bool NeedReward;
    }
}
