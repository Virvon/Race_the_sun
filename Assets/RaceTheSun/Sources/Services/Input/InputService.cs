using System;
using UnityEngine;

namespace Virvon.MyBakery.Services.Input
{
    public class InputService : IInputService
    {
        private readonly GameInputAction _gameInputAction;

        private bool _isDirectionMovementStarted;

        public InputService()
        {
            _gameInputAction = new GameInputAction();
            _gameInputAction.Enable();
            _isDirectionMovementStarted = false;

            _gameInputAction.Player.Jump.performed += _ => Jumped?.Invoke();
            _gameInputAction.Player.MovementDirectionInput.started += _ => _isDirectionMovementStarted = true;
            _gameInputAction.Player.MovementDirectionInput.canceled += _ => _isDirectionMovementStarted = false;
        }

        public event Action Jumped;

        public Vector2 Direction => _isDirectionMovementStarted ? _gameInputAction.Player.MovementDirectionInput.ReadValue<Vector2>() : Vector2.zero;
    }
}
