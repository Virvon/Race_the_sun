namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class FloatTimeInfoButton : StatInfoButton
    {
        protected override string GetInfo()
        {
            float floatTimeValue = PersistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(CurrentSpaceshipType).FloatTime.Value;
            float floatTimeUpgradeValue = StaticDataService.GetSpaceship(CurrentSpaceshipType).FloatTime.UpgradeValue;

            return $"Время полета ({floatTimeValue}с., +{floatTimeUpgradeValue}с. за уровень)";
        }
    }
}