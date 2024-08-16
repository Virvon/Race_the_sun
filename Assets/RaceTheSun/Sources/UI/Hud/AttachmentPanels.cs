using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Animations
{
    public class AttachmentPanels : HudAnimationElement
    {
        [SerializeField] private AttachmentPanel[] _attachmentPanels;

        private List<AttachmentPanel> _usedPanels;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, Attachment.Attachment attachment)
        {
            _usedPanels = new();

            List<Upgrading.UpgradeType> usedTypes = persistentProgressService.Progress.AvailableSpaceships.GetCurrentSpaceshipData().UpgradeTypes;

            for (int i = 0; i < usedTypes.Count; i++)
            {
                Debug.Log(usedTypes[i]);
                _usedPanels.Add(_attachmentPanels[i]);
                _attachmentPanels[i].Init(attachment.GetIcon(usedTypes[i]));
            }
        }

        public override void Open()
        {
            foreach (var panel in _usedPanels)
                panel.Open();
        }

        public override void Hide()
        {
            foreach (var panel in _usedPanels)
                panel.Hide();
        }
    }
}
