using Assets.RaceTheSun.Sources.GameLogic.Animations;
using Assets.RaceTheSun.Sources.GameLogic.Audio;
using Assets.RaceTheSun.Sources.Services.TimeScale;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud
{
    public class PausePanel : HudAnimationElement
    {
        [SerializeField] private JumpPanel _jumpPanel;

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
            _jumpPanel.Hide();
            base.Open();
        }

        public override void Hide()
        {
            _timeScale.Scale(TimeScaleType.Normal);
            _stageMusic.Play();
            _jumpPanel.TryActive();
            base.Hide();
        }
    }
}
