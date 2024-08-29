using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI
{
    public class CameraButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _fromFirstPersonText;
        [SerializeField] private string _fromThirdPersonText;
        
        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

            _persistentProgressService.Progress.SpaceshipMainCameraSettings.Changed += ChangeText;
            _button.onClick.AddListener(OnButtonClick);

            ChangeText();
        }

        private void OnDestroy()
        {
            _persistentProgressService.Progress.SpaceshipMainCameraSettings.Changed += ChangeText;
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _persistentProgressService.Progress.SpaceshipMainCameraSettings.Change(!_persistentProgressService.Progress.SpaceshipMainCameraSettings.IsFromThirdPerson);
        }


        private void ChangeText()
        {
            _text.text = _persistentProgressService.Progress.SpaceshipMainCameraSettings.IsFromThirdPerson ? _fromThirdPersonText : _fromFirstPersonText;
        }
    }
}
