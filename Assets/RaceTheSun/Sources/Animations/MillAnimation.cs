using System.Collections;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Animations
{
    public class MillAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _maxWaitingTime;

        private IEnumerator Start()
        {
            float waitingTime = Random.Range(0, _maxWaitingTime);

            yield return new WaitForSeconds(waitingTime);

            _animator.SetTrigger(AnimationPath.Move);
        }
    }
}
