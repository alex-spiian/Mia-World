using System;
using UnityEngine;

namespace MiraWorld.InputHandler
{
    public class StandaloneInputHandler : IInputHandler
    {
        public event Action PointerDown;
        public event Action PointerUp;
        
        public void Tick()
        {
            if (IsPointerDown())
            {
                PointerDown?.Invoke();
            }

            if (IsPointerUp())
            {
                PointerUp?.Invoke();
            }
        }
        
        public bool IsPointerDown()
        {
            return Input.GetMouseButtonDown(0);
        }

        public bool IsPointerDragging()
        {
            return Input.GetMouseButton(0);
        }

        public bool IsPointerUp()
        {
            return Input.GetMouseButtonUp(0);
        }

        public Vector3 GetPointerPosition()
        {
            return Input.mousePosition;
        }
    }
}