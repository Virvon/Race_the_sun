using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class OpenShopButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IWindowService _windowService;

        [Inject]
        private void Construct(IWindowService windowService)
        {
            _windowService = windowService;
            _button.onClick.AddListener(OpenShop);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OpenShop);
        }

        private void OpenShop()
        {
            _windowService.Open(WindowId.Shop);
        }
    }
}
