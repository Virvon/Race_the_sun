using Assets.RaceTheSun.Sources.GameLogic.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.Hud.AttachmentPanel
{
    public class AttachmentPanel : HudAnimationElement
    {
        [SerializeField] private Image _icon;

        public void SetSprite(Sprite sprite) =>
            _icon.sprite = sprite;
    }
}
