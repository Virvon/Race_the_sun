using System.Collections.Generic;
using UnityEngine;

namespace Assets.MyBakery.Sources.Services.StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/Level", order = 51)]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        //public List<EquipmentSpawnerStaticData> EquipmentSpawners;
    }
}