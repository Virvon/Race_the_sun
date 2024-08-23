using Assets.RaceTheSun.Sources.Services.WaitingService;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class PerfectStagePanel : MonoBehaviour
    {
        [SerializeField] private JumpPanel _jumpPanel;

        private IWaitingService _waitingService;

        [Inject]
        private void Construct(IWaitingService waitingService)
        {
            _waitingService = waitingService;
        }

        public void Show()
        {
            bool isJumpPanelActivated = _jumpPanel.IsActivated;

            _jumpPanel.Hide();
            gameObject.SetActive(true);

            _waitingService.Wait(4, callback: () =>
            {
                gameObject.SetActive(false);

                if (isJumpPanelActivated)
                    _jumpPanel.TryActive();
            });
        }
    }
}
