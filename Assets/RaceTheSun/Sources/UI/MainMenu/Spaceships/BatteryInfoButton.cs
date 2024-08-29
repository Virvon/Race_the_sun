using UnityEngine;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
{
    public class BatteryInfoButton : StatInfoButton
    {
        protected override string GetInfo()
        {
            float bateryValue = PersistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(CurrentSpaceshipType).Battery.Value;
            float batteryUpgradeValue = StaticDataService.GetSpaceship(CurrentSpaceshipType).Battery.UpgradeValue;

            return $"Сохраняй заряд в тени дольше ({bateryValue}с., +{batteryUpgradeValue}с. за уровень)";
        }
    }
}