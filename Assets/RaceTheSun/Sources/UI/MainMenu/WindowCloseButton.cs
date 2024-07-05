namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class WindowCloseButton : WindowInteractionButton
    {
        protected override void Interact() =>
            OpenableWindow.Close();
    }
}
