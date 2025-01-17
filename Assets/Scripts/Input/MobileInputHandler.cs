using System;
using UnityEngine;

namespace MiraWorld.InputHandler
{
    public class MobileInputHandler : IInputHandler
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
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }

        public bool IsPointerDragging()
        {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
        }

        public bool IsPointerUp()
        {
            return Input.touchCount > 0 &&
                   (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled);
        }

        public Vector3 GetPointerPosition()
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).position;
            }

            return Vector3.zero;
        }
    }
}