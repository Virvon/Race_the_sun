namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class ExperienceMultiplierInfoButton : StatInfoButton
    {
        protected override string GetInfo()
        {
            float experienceMultiplierValue = PersistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(CurrentSpaceshipType).ExperienceMultiplier.Value * 100;
            float experienceMultiplierUpgradeValue = StaticDataService.GetSpaceship(CurrentSpaceshipType).ExperienceMultiplier.UpgradeValue * 100;

            return $"Множитель опыта ({experienceMultiplierValue}%, +{experienceMultiplierUpgradeValue}% за уровень";
        }
    }
}