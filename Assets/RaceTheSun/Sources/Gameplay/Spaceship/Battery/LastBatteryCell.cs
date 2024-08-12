using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class LastBatteryCell : BatteryCell
    {
        private readonly Material _dischargedMaterial;

        public LastBatteryCell(float minIncludeValue, Material chargedMaterial, Material lowBatteryMaterial, Material dischargedMaterial, int materialIndex) : base(minIncludeValue, chargedMaterial, lowBatteryMaterial, materialIndex)
        {
            _dischargedMaterial = dischargedMaterial;
        }

        public override bool IsNeedToChangeMaterial(float batteryValue, out Material material)
        {
            if(batteryValue == 0)
            {
                material = _dischargedMaterial;
                CurrentMaterial = material;
                return true;
            }

            return base.IsNeedToChangeMaterial(batteryValue, out material);
        }
    }
}
