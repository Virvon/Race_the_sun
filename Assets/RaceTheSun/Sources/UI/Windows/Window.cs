using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Windows
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Awake() =>
            OnAwake();

        private void Start() =>
            SubscribeUpdates();

        private void OnDestroy() =>
            Cleanup();


        protected virtual void OnAwake() =>
            _closeButton.onClick.AddListener(() => Destroy(gameObject));

        protected virtual void Init() { }

        protected virtual void SubscribeUpdates() { }

        protected virtual void Cleanup() { }

        public class Factory : PlaceholderFactory<AssetReferenceGameObject, UniTask<Window>>
        {
        }
    }
}
