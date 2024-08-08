using UnityEngine;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [CreateAssetMenu(fileName = "GameplayWorldConfig", menuName = "StaticData/Create new gameplay world config", order = 51)]
    public class GameplayWorldConfig : ScriptableObject
    {
        public int CellLength;
        public int RenderDistacne;

        public StageConfig[] StageConfigs;
    }
}
