using UnityEngine;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class BatteryInfoButton : StatInfoButton
    {
        protected override string GetInfo()
        {
            return $"Сохраняй заряд в тени дольше ({PersistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(CurrentSpaceshipType).Battery.Value}с., +{StaticDataService.GetSpaceship(CurrentSpaceshipType).Battery.UpgradeValue}с. за уровень)";
        }
    }
}