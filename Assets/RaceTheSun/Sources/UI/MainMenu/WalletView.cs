using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _walletValue;

        private IPersistentProgressService _persistentProgress;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgress = persistentProgressService;

            _walletValue.text = _persistentProgress.Progress.Wallet.Value.ToString();
            _persistentProgress.Progress.Wallet.ValueChanged += OnWalletValueChanged;
        }

        private void OnDestroy()
        {
            _persistentProgress.Progress.Wallet.ValueChanged -= OnWalletValueChanged;
        }

        private void OnWalletValueChanged(int value)
        {
            _walletValue.text = value.ToString();
        }
    }
}