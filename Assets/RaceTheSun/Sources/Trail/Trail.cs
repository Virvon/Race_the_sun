using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Trail
{
    public class Trail : MonoBehaviour
    {
        private Vector3 _targetLookPoint;

        private void Start()
        {
            _targetLookPoint = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 1);
        }

        private void Update()
        {
            if (_targetLookPoint == null)
                return;

            transform.rotation = Quaternion.LookRotation(transform.localPosition - _targetLookPoint);
        }

        public class Factory : PlaceholderFactory<AssetReferenceGameObject, UniTask<Trail>>
        {
        }
    }
}
