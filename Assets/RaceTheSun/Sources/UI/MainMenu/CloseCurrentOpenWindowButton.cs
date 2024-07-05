using UnityEngine;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class CloseCurrentOpenWindowButton : WindowInteractionButton
    {
        [SerializeField] private OpenableWindow _currentWindow;

        protected override void Interact()
        {
            _currentWindow.Close();
            OpenableWindow.Open();
        }
    }
}
