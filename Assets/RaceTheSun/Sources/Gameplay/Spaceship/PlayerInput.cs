using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Test
{
    public class PlayerInput : MonoBehaviour
    {
        public  Vector2 MoveInput { get; private set; }

        private void Update()
        {
            MoveInput = Vector2.right * Input.GetAxis("Horizontal");
        }
    }
}
