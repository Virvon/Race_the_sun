using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class SpaceshipMainCameraSettings
    {
        public bool IsFromThirdPerson;

        public SpaceshipMainCameraSettings() =>
            IsFromThirdPerson = true;

        public event Action Changed;

        public void Change(bool isFromThirdPerson)
        {
            IsFromThirdPerson = isFromThirdPerson;
            Changed?.Invoke();
        }
    }
}