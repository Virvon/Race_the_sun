using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Audio
{
    public class CollectItemsSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _takeScoreItemAudioSource;
        [SerializeField] private AudioSource _takeItemAudioSource;

        public void TakeScoreItem()
        {
            _takeScoreItemAudioSource.Play();
        }

        public void TakeItem()
        {
            _takeItemAudioSource.Play();
        }

        public class Factory : PlaceholderFactory<string, UniTask<CollectItemsSoundEffects>>
        {

        }
    }
}
