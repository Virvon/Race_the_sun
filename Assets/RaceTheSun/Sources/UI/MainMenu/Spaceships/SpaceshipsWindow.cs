using System;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class SpaceshipsWindow : OpenableWindow
    {
        public override void Close()
        {
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }
    }
}