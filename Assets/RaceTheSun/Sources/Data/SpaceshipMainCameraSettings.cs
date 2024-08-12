using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class SpaceshipMainCameraSettings
    {
        public bool IsFromThirdPerson;

        public event Action Changed;

        public SpaceshipMainCameraSettings()
        {
            IsFromThirdPerson = true;
        }

        public void Change(bool isFromThirdPerson)
        {
            IsFromThirdPerson = isFromThirdPerson;
            Changed?.Invoke();
        }
    }
}