using System;

namespace Assets.RaceTheSun.Sources.UI.LoadingCurtain
{
    public interface ILoadingCurtain
    {
        void Hide(float duration = 0, Action callback = null);
        void Show(float duration = 0, Action callback = null);
    }
}