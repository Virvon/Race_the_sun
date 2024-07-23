using System;

namespace Assets.RaceTheSun.Sources.Services.WaitingService
{
    public interface IWaitingService
    {
        void Wait(float delay, Action callback);
    }
}