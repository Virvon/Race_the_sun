using UnityEngine;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public abstract class OpenableWindow : MonoBehaviour
    {
        public abstract void Open();
        public abstract void Close();
    }
}
