namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class LevelProgressButton : InformationButton
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            ShowedInformation += OnShowedInformation;
        }

        protected override void OnDisable()
        {
            base.OnEnable();
            ShowedInformation -= OnShowedInformation;
        }

        private void OnShowedInformation(InformationButton _)
        {
            OpenInfo();
        }
    }
}