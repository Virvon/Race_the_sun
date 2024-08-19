using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class BatteryCell
    {
        protected readonly float MinIncludeValue;
        protected readonly Material DischargedMaterial;
        protected readonly Material ChargedMaterial;

        public BatteryCell(float minIncludeValue, Material chargedMaterial, Material dischargedMaterial, int materialIndex)
        {
            MinIncludeValue = minIncludeValue;
            ChargedMaterial = chargedMaterial;
            DischargedMaterial = dischargedMaterial;
            MaterialIndex = materialIndex;
        }

        protected Material CurrentMaterial;

        public int MaterialIndex { get; private set; }

        public virtual bool IsNeedToChangeMaterial(float batteryValue, out Material material)
        {
            material = null;

            if(batteryValue < MinIncludeValue && CurrentMaterial != DischargedMaterial)
            {
                material = DischargedMaterial;
                CurrentMaterial = material;
                return true;
            }
            else if(batteryValue >= MinIncludeValue && CurrentMaterial != ChargedMaterial)
            {
                material = ChargedMaterial;
                CurrentMaterial = material;
                return true;
            }

            return false;
        }
    }
}
