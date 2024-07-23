using Assets.RaceTheSun.Sources.Services.TimeScale;
using Zenject;

namespace Assets.RaceTheSun.Sources.Animations
{
    public class PausePanel : HudAnimationElement
    {
        private ITimeScale _timeScale;

        [Inject]
        private void Construct(ITimeScale timeSclale) =>
            _timeScale = timeSclale;

        public override void Open()
        {
            _timeScale.Scale(TimeScaleType.Pause);
            base.Open();
        }

        public override void Hide()
        {
            _timeScale.Scale(TimeScaleType.Normal);
            base.Hide();
        }
    }
}
