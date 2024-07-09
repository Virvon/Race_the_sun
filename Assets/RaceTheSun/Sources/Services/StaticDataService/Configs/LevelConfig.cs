using UnityEngine;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "StaticData/Create new level config", order = 51)]
    public class LevelConfig : ScriptableObject
    {
        public StageConfig[] StageConfigs;
    }
}
