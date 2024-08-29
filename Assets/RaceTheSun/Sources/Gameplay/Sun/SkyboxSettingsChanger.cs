using System.Collections;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.StageInfo;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Sun
{
    public class SkyboxSettingsChanger : MonoBehaviour
    {
        private const string SkyboxColor = "_SkyTint";
        private const string AtmosphereThickness = "_AtmosphereThickness";
        private const string Exposure = "_Exposure";

        [SerializeField] private SkyboxConfig _startSkyboxConfig;
        [SerializeField] private float _changeColorDuration;

        private CurrentSpaceshipStage _currentSpaceshipStage;
        private IStaticDataService _staticDataService;
        private Material _skyboxMaterial;

        [Inject]
        private void Construct(CurrentSpaceshipStage currentSpaceshipStage, IStaticDataService staticDataService)
        {
            _currentSpaceshipStage = currentSpaceshipStage;
            _staticDataService = staticDataService;
            _skyboxMaterial = RenderSettings.skybox;
        }

        public void Reset()
        {
            SetSkyboxMaterialSettings(
                _startSkyboxConfig.SkyboxColor,
                _startSkyboxConfig.AtmosphereThickness,
                _startSkyboxConfig.Exposure);
        }

        private void Start() =>
            Reset();

        private void OnEnable() =>
            _currentSpaceshipStage.StageChanged += ChangeColor;

        private void OnDisable() =>
            _currentSpaceshipStage.StageChanged -= ChangeColor;

        private void SetSkyboxMaterialSettings(Color color, float atmosphereThickness, float exposure)
        {
            _skyboxMaterial.SetColor(SkyboxColor, color);
            _skyboxMaterial.SetFloat(AtmosphereThickness, atmosphereThickness);
            _skyboxMaterial.SetFloat(Exposure, exposure);
        }

        private void ChangeColor(Stage stage)
        {
            SkyboxConfig skyboxConfig = _staticDataService.GetStage(stage).Skybox;

            StartCoroutine(SkyboxChanger(skyboxConfig.SkyboxColor, skyboxConfig.AtmosphereThickness, skyboxConfig.Exposure));
        }

        private IEnumerator SkyboxChanger(Color targerColor, float targetAtmosphereThickness, float targetExposure)
        {
            float passedTime = 0;
            float progress = 0;

            Color startColor = _skyboxMaterial.GetColor(SkyboxColor);
            Color currentColor;

            float startAtmosphereThickness = _skyboxMaterial.GetFloat(AtmosphereThickness);
            float currentAtmosphereThickness = startAtmosphereThickness;

            float startExposure = _skyboxMaterial.GetFloat(Exposure);
            float currentExposure = startExposure;  

            while (progress < 1)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _changeColorDuration;

                currentColor = Color.Lerp(startColor, targerColor, progress);
                currentAtmosphereThickness = Mathf.Lerp(startAtmosphereThickness, targetAtmosphereThickness, progress);
                currentExposure = Mathf.Lerp(startExposure, targetExposure, progress);

                _skyboxMaterial.SetColor(SkyboxColor, currentColor);
                _skyboxMaterial.SetFloat(AtmosphereThickness, currentAtmosphereThickness);
                _skyboxMaterial.SetFloat(Exposure, currentExposure);

                yield return null;
            }
        }
    }
}
