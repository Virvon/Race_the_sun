namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class PickUpRangeInfoButton : StatInfoButton
    {
        protected override string GetInfo()
        {
            float pickUpRangeValue = PersistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(CurrentSpaceshipType).PickUpRange.Value;
            float pickUpRangeUpgradeValue = StaticDataService.GetSpaceship(CurrentSpaceshipType).PickUpRange.UpgradeValue;

            return $"Радиус подбора ({pickUpRangeValue}, +{pickUpRangeUpgradeValue} за уровень)";
        }
    }
}