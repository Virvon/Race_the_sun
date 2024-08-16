using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.Animations
{
    public class AttachmentPanel : HudAnimationElement
    {
        [SerializeField] private Image _icon;

        public void Init(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}
