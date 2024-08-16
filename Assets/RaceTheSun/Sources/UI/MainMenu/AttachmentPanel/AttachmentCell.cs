using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Upgrading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class AttachmentCell : MonoBehaviour
    {
        [SerializeField] private UpgradeType _cellType;
        [SerializeField] private GameObject _blockIcon;
        [SerializeField] private Image _icon;

        private IPersistentProgressService _persistentProgressService;
        private Attachment.Attachment _attachment;
        private UpgradeType _usedUpgradeType;
        private bool _isBlocked;
        private bool _isUsed;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, Attachment.Attachment attachment)
        {
            _persistentProgressService = persistentProgressService;
            _attachment = attachment;

            _isBlocked = _persistentProgressService.Progress.Upgrading.IsUpgraded(_cellType) == false;
            _isUsed = false;

            _blockIcon.SetActive(_isBlocked);
        }

        public void Reset()
        {
            _isUsed = false;
            _icon.gameObject.SetActive(false);
        }

        public bool TryUse(UpgradeType upgradeType, SpaceshipType spaceshipType)
        {
            if (_isUsed || _isBlocked)
                return false;

            _usedUpgradeType = upgradeType;
            
            if(_persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(spaceshipType).UpgradeTypes.Contains(upgradeType) == false)
                _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(spaceshipType).UpgradeTypes.Add(_usedUpgradeType);

            _isUsed = true;
            _icon.sprite = _attachment.GetIcon(_usedUpgradeType);
            _icon.gameObject.SetActive(true);

            return true;
        }

        public bool TryRemove(UpgradeType upgradeType, SpaceshipType spaceshipType)
        {
            if (_isBlocked || upgradeType != _usedUpgradeType || _isUsed == false)
                return false;

            _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(spaceshipType).UpgradeTypes.Remove(_usedUpgradeType);
            _isUsed = false;
            _icon.gameObject.SetActive(false);

            return true;
        }
    }
}