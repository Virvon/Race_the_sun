using Assets.RaceTheSun.Sources.Services.WaitingService;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud
{
    public class PerfectStagePanel : MonoBehaviour
    {
        private const int ShowDuration = 4;

        [SerializeField] private JumpPanel _jumpPanel;

        private IWaitingService _waitingService;

        [Inject]
        private void Construct(IWaitingService waitingService) =>
            _waitingService = waitingService;

        public void Show()
        {
            bool isJumpPanelActivated = _jumpPanel.IsActivated;

            _jumpPanel.Hide();
            gameObject.SetActive(true);

            _waitingService.Wait(ShowDuration, callback: () =>
            {
                gameObject.SetActive(false);

                if (isJumpPanelActivated)
                    _jumpPanel.TryActive();
            });
        }
    }
}
