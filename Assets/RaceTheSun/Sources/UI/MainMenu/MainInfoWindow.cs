namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class MainInfoWindow : OpenableWindow
    {
        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }
    }
}