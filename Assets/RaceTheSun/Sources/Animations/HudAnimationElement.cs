using Assets.RaceTheSun.Sources.UI.MainMenu;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Animations
{
    public class HudAnimationElement : OpenableWindow
    {
        [SerializeField] private Animator _animator;

        private bool _isOpened;
        private bool _isActivated;

        private void Start()
        {
            _isOpened = true;
            _isActivated = false;
        }

        public override void Open()
        {
            if (_isActivated == false && gameObject.activeSelf)
            {
                _isActivated = true;
                _animator.SetTrigger(AnimationPath.ActivateElement);
            }

            SetOpen(true);
        }

        public override void Hide()
        {
            SetOpen(false);
        }

        private void SetOpen(bool isOpened)
        {
            if(_isOpened == isOpened || gameObject.activeSelf == false)
                return;

            _isOpened = isOpened;
            _animator.SetBool(AnimationPath.IsOpened, _isOpened);
        }
    }
}
