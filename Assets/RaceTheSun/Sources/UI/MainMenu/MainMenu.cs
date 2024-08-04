using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private GameInputAction _input;

        private void OnEnable()
        {
            _input = new GameInputAction();
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            Debug.Log(_input.Player.Swipe.ReadValue<Vector2>());
        }
        public class Factory : PlaceholderFactory<string, UniTask<MainMenu>>
        {
        }
    }
}
