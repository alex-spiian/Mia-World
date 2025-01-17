using MiraWorld.CameraControl;
using MiraWorld.InputHandler;
using MiraWorld.Item;
using UnityEngine;

namespace MiraWorld.Core
{
    public class BootstrapEntryPoint : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private ItemSpawner _itemSpawner;
        
        private IInputHandler _inputHandler;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _inputHandler = GetInputHandler();
            _cameraController.Initialize(_inputHandler);
            _itemSpawner.Initialize(_inputHandler);
            _itemSpawner.Spawn();
        }

        private void Update()
        {
            _inputHandler.Tick();
        }

        private IInputHandler GetInputHandler()
        {
#if UNITY_ANDROID || UNITY_IOS
            return new MobileInputHandler();
#else
            return new StandaloneInputHandler();
#endif
        }
    }
}