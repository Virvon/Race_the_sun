namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class FloatTimeInfoButton : StatInfoButton
    {
        protected override string GetInfo() =>
            $"Время полета ({PersistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(CurrentSpaceshipType).FloatTime.Value}с., +{StaticDataService.GetSpaceship(CurrentSpaceshipType).FloatTime.UpgradeValue}с. за уровень)";
    }
}