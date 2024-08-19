﻿using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class Battery : MonoBehaviour
    {
        private const float FullBattery = 1;

        [SerializeField] private float _dischargerDuration;

        private Coroutine _batteryDischarger;
        private float _battery;

        public event Action<float> BatteryValueChanged;

        public bool Discharged => _battery == 0;
        public float Value => _battery;

        private void Start()
        {
            _battery = FullBattery;
        }

        public void ChangeShadowed(bool isShadowed)
        {
            if (isShadowed)
            {
                _batteryDischarger = StartCoroutine(BatteryDischarger());
            }
            else
            {
                if(_batteryDischarger != null)
                    StopCoroutine(_batteryDischarger);

                _battery = FullBattery;
                BatteryValueChanged?.Invoke(_battery);
            }
        }

        private IEnumerator BatteryDischarger()
        {
            float progress = 0;
            float time = 0;

            while(progress < 1)
            {
                time += Time.deltaTime;
                progress = time / _dischargerDuration;

                _battery = Mathf.Lerp(FullBattery, 0, progress);
                BatteryValueChanged?.Invoke(_battery);

                yield return null;
            }
        }
    }
}
