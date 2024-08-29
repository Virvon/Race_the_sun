namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class ExperienceMultiplierInfoButton : StatInfoButton
    {
        protected override string GetInfo()
        {
            float ExperienceMultiplierValue = PersistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(CurrentSpaceshipType).ExperienceMultiplier.Value * 100;
            float ExperienceMultiplierUpgradeValue = StaticDataService.GetSpaceship(CurrentSpaceshipType).ExperienceMultiplier.UpgradeValue * 100;

            return $"Множитель опыта ({ExperienceMultiplierValue}%, +{ExperienceMultiplierUpgradeValue}% за уровень";
        }
    }
}