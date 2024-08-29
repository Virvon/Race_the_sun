namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class ExperienceMultiplierInfoButton : StatInfoButton
    {
        protected override string GetInfo() =>
            $"Множитель опыта ({PersistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(CurrentSpaceshipType).ExperienceMultiplier.Value * 100}%, +{StaticDataService.GetSpaceship(CurrentSpaceshipType).ExperienceMultiplier.UpgradeValue * 100}% за уровень";
    }
}