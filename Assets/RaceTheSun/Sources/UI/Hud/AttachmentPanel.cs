using Assets.RaceTheSun.Sources.GameLogic.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.Hud
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
