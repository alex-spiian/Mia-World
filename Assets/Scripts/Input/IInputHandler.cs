using System;
using UnityEngine;

namespace MiraWorld.InputHandler
{
    public interface IInputHandler
    {
        public event Action PointerDown;
        public event Action PointerUp;
        
        public void Tick();

        public bool IsPointerDown();
        public bool IsPointerDragging();
        public bool IsPointerUp();
        public Vector3 GetPointerPosition();
    }
}