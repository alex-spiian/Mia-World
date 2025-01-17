using MiraWorld.InputHandler;
using UnityEngine;

namespace MiraWorld.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraConfig _cameraConfig;

        private bool _canScroll;
        private IInputHandler _inputHandler;

        public void Initialize(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _inputHandler.PointerDown += OnPointerDown;
            _inputHandler.PointerUp += OnPointerUp;
        }
        
        private void Update()
        {
            if (!_canScroll)
                return;

            var screenEdgeThreshold = _cameraConfig.ScreenEdgeScrollThreshold;
            var mouseX = Input.mousePosition.x;

            if (mouseX < Screen.width * screenEdgeThreshold)
            {
                ScrollCamera(-1);
            }
            
            else if (mouseX > Screen.width * (1 - screenEdgeThreshold))
            {
                ScrollCamera(1);
            }
        }

        private void OnDestroy()
        {
            _inputHandler.PointerDown -= OnPointerDown;
            _inputHandler.PointerUp -= OnPointerUp;
        }

        private void ScrollCamera(float direction)
        {
            var position = transform.position;
            position.x += direction * _cameraConfig.ScrollSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, _cameraConfig.BoundaryLeft, _cameraConfig.BoundaryRight);
            transform.position = position;
        }

        private void OnPointerDown()
        {
            _canScroll = true;
        }
        
        private void OnPointerUp()
        {
            _canScroll = false;
        }
    }
}