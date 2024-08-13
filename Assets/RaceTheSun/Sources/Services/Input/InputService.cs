using System;
using UnityEngine;

namespace Virvon.MyBakery.Services.Input
{
    public class InputService : IInputService
    {
        private readonly GameInputAction _gameInputAction;

        public event Action Jumped;

        public InputService()
        {
            _gameInputAction = new GameInputAction();
            _gameInputAction.Enable();

            _gameInputAction.Player.Jump.performed += ctx => Jumped?.Invoke();
        }

        public Vector2 Direction => _gameInputAction.Player.MovementDirectionInput.ReadValue<Vector2>();
    }
}
