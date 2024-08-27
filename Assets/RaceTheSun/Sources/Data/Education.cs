using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class Education
    {
        public bool IsSpaceshipWindowShowed;
        public bool IsShopWindowShowed;

        public Education()
        {
            IsSpaceshipWindowShowed = false;
            IsShopWindowShowed = false;
        }
    }
}