using Assets.RaceTheSun.Sources.Services.CoroutineRunner;
using System.Collections;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class LastBatteryCell : BatteryCell
    {
        private const float BlinkingSpeed = 0.3f;

        private readonly Material _dischargedMaterial;
        private readonly ICoroutineRunner _coroutineRunner;

        private bool _isBlinked;
        private Material _currentBlinkingMaterial;

        public LastBatteryCell(float minIncludeValue, Material chargedMaterial, Material lowBatteryMaterial, Material dischargedMaterial, int materialIndex, ICoroutineRunner coroutineRunner) : base(minIncludeValue, chargedMaterial, lowBatteryMaterial, materialIndex)
        {
            _dischargedMaterial = dischargedMaterial;
            _isBlinked = false;
            _coroutineRunner = coroutineRunner;

            Debug.Log("Last battery");
            Debug.Log("_discharged mat " + (_dischargedMaterial != null));
        }

        public override bool IsNeedToChangeMaterial(float batteryValue, out Material material)
        {
            material = null;

            if(batteryValue == 0)
            {
                material = _dischargedMaterial;
                CurrentMaterial = material;
                _isBlinked = false;
                return true;
            }
            else if(batteryValue < MinIncludeValue)
            {
                if(_isBlinked == false)
                    _coroutineRunner.StartCoroutine(Blinking());

                material = _currentBlinkingMaterial;

                return true;
            }
            else if(batteryValue >= MinIncludeValue && CurrentMaterial != ChargedMaterial)
            {
                material = ChargedMaterial;
                _isBlinked = false;
                return true;
            }

            return false;
        }

        private IEnumerator Blinking()
        {
            _isBlinked = true;
            _currentBlinkingMaterial = DischargedMaterial;

            float passedTime = 0;

            while(_isBlinked)
            {
                passedTime += Time.deltaTime;

                if(passedTime >= BlinkingSpeed)
                {
                    passedTime = 0;

                    _currentBlinkingMaterial = _currentBlinkingMaterial == DischargedMaterial? _dischargedMaterial : DischargedMaterial;
                }

                yield return null;
            }
        }
    }
}
