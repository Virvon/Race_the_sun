﻿using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class BatteryIndicator : MonoBehaviour
    {
        [SerializeField] private Battery _battery;

        private MeshRenderer _meshRenderer;
        private List<BatteryMaterialInfo> _batteryMaterialsInfo;
        private Material _chargedMaterial;
        private Material _dischargedMaterial;
        private Material _lowBatteryMaterial;
        private List<BatteryCell> _cells;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistentProgressService persistentProgressService)
        {
            SpaceshipConfig spaceshipConfig = staticDataService.GetSpaceship(persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType);

            _batteryMaterialsInfo = spaceshipConfig.BatteryMaterialsInfo;
            _chargedMaterial = spaceshipConfig.ChargedBatteryMaterial;
            _dischargedMaterial = spaceshipConfig.DischargedBatteryMaterial;
            _lowBatteryMaterial = spaceshipConfig.LowBatteryMaterial;

            _battery.BatteryValueChanged += OnBatteryValueChanged;

            _cells = new();

           for(int i = 0; i < _batteryMaterialsInfo.Count - 1; i++)
           {
                _cells.Add(new BatteryCell(GetMinIncludeValue(_batteryMaterialsInfo[i].Position, _batteryMaterialsInfo.Count), _chargedMaterial, _dischargedMaterial, _batteryMaterialsInfo[i].Index));
           }

            _cells.Add(new LastBatteryCell(GetMinIncludeValue(_batteryMaterialsInfo.Last().Position, _batteryMaterialsInfo.Count) , _chargedMaterial, _lowBatteryMaterial, _dischargedMaterial, _batteryMaterialsInfo.Last().Index));
        }

        private void OnDestroy()
        {
            _battery.BatteryValueChanged -= OnBatteryValueChanged;
        }

        public void Init(MeshRenderer meshRenderer)
        {
            _meshRenderer = meshRenderer;
        }

        private float GetMinIncludeValue(int position, int cellsCount)
        {
            return 1 - (0.5f / cellsCount) * position;
        }

        private void OnBatteryValueChanged(float value)
        {
            foreach(BatteryCell cell in _cells)
            {
                if(cell.IsNeedToChangeMaterial(value, out Material material))
                {
                    Material[] materials = _meshRenderer.materials;
                    materials[cell.MaterialIndex] = material;
                    _meshRenderer.materials = materials;
                }
            }
        }
    }
}
