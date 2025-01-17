using UnityEngine;

namespace MiraWorld.CameraControl
{
    [CreateAssetMenu(menuName = "ScriptableObject/CameraConfig", fileName = "CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField, Tooltip("Speed at which the camera scrolls.")]
        public float ScrollSpeed { get; private set; }

        [field: SerializeField, Tooltip("Left boundary for camera movement.")]
        public float BoundaryLeft { get; private set; }

        [field: SerializeField, Tooltip("Right boundary for camera movement.")]
        public float BoundaryRight { get; private set; }

        [field: SerializeField, Tooltip("Percentage of screen width to trigger edge scrolling.")]
        public float ScreenEdgeScrollThreshold { get; private set; }

        [field: SerializeField, Tooltip("Sensitivity of drag scrolling.")]
        public float DragScrollSensitivity { get; private set; }
    }
}