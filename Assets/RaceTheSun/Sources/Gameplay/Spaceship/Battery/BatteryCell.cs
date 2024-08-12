using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class BatteryCell
    {
        private readonly float _minIncludeValue;
        private readonly Material _chargedMaterial;
        private readonly Material _dischargedMaterial;

        public BatteryCell(float minIncludeValue, Material chargedMaterial, Material dischargedMaterial, int materialIndex)
        {
            _minIncludeValue = minIncludeValue;
            _chargedMaterial = chargedMaterial;
            _dischargedMaterial = dischargedMaterial;
            MaterialIndex = materialIndex;
        }

        protected Material CurrentMaterial;

        public int MaterialIndex { get; private set; }

        public virtual bool IsNeedToChangeMaterial(float batteryValue, out Material material)
        {
            material = null;

            if(batteryValue < _minIncludeValue && CurrentMaterial != _dischargedMaterial)
            {
                material = _dischargedMaterial;
                CurrentMaterial = material;
                return true;
            }
            else if(batteryValue >= _minIncludeValue && CurrentMaterial != _chargedMaterial)
            {
                material = _chargedMaterial;
                CurrentMaterial = material;
                return true;
            }

            return false;
        }
    }
}
