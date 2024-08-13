using System;
using UnityEngine;

namespace Virvon.MyBakery.Services.Input
{
    public interface IInputService
    {
        event Action Jumped;
        Vector2 Direction { get; }
    }
}
