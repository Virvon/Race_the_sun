using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public abstract class WindowInteractionButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private OpenableWindow _openableWindow;

        protected OpenableWindow OpenableWindow => _openableWindow;

        private void OnEnable() =>
            _button.onClick.AddListener(Interact);

        private void OnDisable() =>
            _button.onClick.RemoveListener(Interact);

        protected abstract void Interact();
    }
}
