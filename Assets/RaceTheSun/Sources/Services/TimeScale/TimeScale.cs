using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Services.TimeScale
{
    public class TimeScale : ITimeScale
    {
        public void Scale(TimeScaleType timeScaleType) =>
            Time.timeScale = (int)timeScaleType;
    }
}
