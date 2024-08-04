using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetMouseButton(0))
                Debug.Log("Horizontal " + Input.GetAxis("Horizontal") + " Vertical " + Input.GetAxis("Vertical"));
        }
        public class Factory : PlaceholderFactory<string, UniTask<MainMenu>>
        {
        }
    }
}
