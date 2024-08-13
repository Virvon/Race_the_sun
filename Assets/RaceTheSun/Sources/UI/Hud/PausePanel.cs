using Assets.RaceTheSun.Sources.Audio;
using Assets.RaceTheSun.Sources.Services.TimeScale;
using Zenject;

namespace Assets.RaceTheSun.Sources.Animations
{
    public class PausePanel : HudAnimationElement
    {
        private ITimeScale _timeScale;
        private StageMusic _stageMusic;

        [Inject]
        private void Construct(ITimeScale timeSclale, StageMusic stageMusic)
        {
            _timeScale = timeSclale;
            _stageMusic = stageMusic;

        }

        public override void Open()
        {
            _timeScale.Scale(TimeScaleType.Pause);
            _stageMusic.Pause();
            base.Open();
        }

        public override void Hide()
        {
            _timeScale.Scale(TimeScaleType.Normal);
            _stageMusic.Play();
            base.Hide();
        }
    }
}
