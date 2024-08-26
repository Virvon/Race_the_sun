using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [Serializable]
    public class StageConfig
    {
        public Stage Stage;
        public SkyboxConfig Skybox;
        public AssetReferenceGameObject[] Tiles;
    }
}
