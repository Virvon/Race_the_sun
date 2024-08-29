using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Battery
{
    public class Battery : MonoBehaviour
    {
        private const float FullBattery = 1;

        [SerializeField] private Spaceship _spaceship;

        private float _dischargerDuration;
        private Coroutine _batteryDischarger;
        private float _battery;

        public event Action<float> BatteryValueChanged;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _dischargerDuration = persistentProgressService.Progress.AvailableSpaceships.GetCurrentSpaceshipData().Battery.Value;
            _battery = FullBattery;
        }

        public bool Discharged => _battery == 0;
        public float Value => _battery;

        public void ChangeShadowed(bool isShadowed)
        {
            if (_spaceship.gameObject.activeSelf == false)
                return;

            if (isShadowed)
            {
                _batteryDischarger = StartCoroutine(BatteryDischarger());
            }
            else
            {
                if (_batteryDischarger != null)
                    StopCoroutine(_batteryDischarger);

                _battery = FullBattery;
                BatteryValueChanged?.Invoke(_battery);
            }
        }

        private IEnumerator BatteryDischarger()
        {
            float progress = 0;
            float time = 0;

            while (progress < 1)
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
