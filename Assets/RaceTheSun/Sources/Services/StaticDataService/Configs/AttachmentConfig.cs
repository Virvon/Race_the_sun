using Assets.RaceTheSun.Sources.Upgrading;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [CreateAssetMenu(fileName = "AttachmentConfig", menuName = "StaticData/Create new attachment config", order = 51)]
    public class AttachmentConfig : ScriptableObject
    {
        public UpgradeType AttachmentUpgradeType;
        public Sprite Icon;
        public string Name;
        public string Title;
    }
}
