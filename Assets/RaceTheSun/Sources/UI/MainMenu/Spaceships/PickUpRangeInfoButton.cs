namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class PickUpRangeInfoButton : StatInfoButton
    {
        protected override string GetInfo()
        {
            return $"Радиус подбора ({PersistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(CurrentSpaceshipType).PickUpRange.Value}, +{StaticDataService.GetSpaceship(CurrentSpaceshipType).PickUpRange.UpgradeValue} за уровень)";
        }
    }
}