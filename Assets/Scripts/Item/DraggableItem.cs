using System;
using System.Linq;
using MiraWorld.InputHandler;
using UnityEngine;

namespace MiraWorld.Item
{
    public class DraggableItem : MonoBehaviour
    {
        public event Action<DraggableItem> StartedDragging;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private LayerMask _draggableLayerMask;
        [SerializeField] private int _minSortingOrder;
        [SerializeField] private int _maxSortingOrder;

        private Rigidbody2D _rigidbody;
        private Camera _mainCamera;
        private Transform _transform;
        private IInputHandler _inputHandler;
        private Vector3 _offset;
        private bool _isDragging;
        private bool _isInitialized;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = transform;
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!_isInitialized)
                return;
            
            if (_isDragging)
            {
                DragItem(_inputHandler.GetPointerPosition());
            }
        }

        private void OnDestroy()
        {
            _inputHandler.PointerUp -= OnPointerUp;
            _inputHandler.PointerDown -= OnPointerDown;
        }

        public void Initialize(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _isInitialized = true;

            _inputHandler.PointerUp += OnPointerUp;
            _inputHandler.PointerDown += OnPointerDown;
        }
        
        public void SetSortingOrder(int order)
        {
            _spriteRenderer.sortingOrder = _minSortingOrder + order;
        }
        
        private void OnPointerDown()
        {
            if (IsFirstInOrder())
            {
                StartDragging();
            }
        }

        private void OnPointerUp()
        {
            _isDragging = false;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            SetSortingOrder(_minSortingOrder);
        }

        private void StartDragging()
        {
            var pointerPosition = _inputHandler.GetPointerPosition();
            _offset = _transform.position - GetWorldPosition(pointerPosition);
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _isDragging = true;
            SetSortingOrder(_maxSortingOrder);
            
            StartedDragging?.Invoke(this);
        }

        private void DragItem(Vector3 pointerPosition)
        {
            _transform.position = GetWorldPosition(pointerPosition) + _offset;
        }

        private Vector3 GetWorldPosition(Vector3 screenPosition)
        {
            var worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0;
            return worldPosition;
        }
        
        private bool IsFirstInOrder()
        {
            var pointerPosition = GetWorldPosition(_inputHandler.GetPointerPosition());
            var hits = Physics2D.RaycastAll(pointerPosition, Vector2.zero, 0f, _draggableLayerMask);

            if (hits.Length == 0)
                return false;

            var firstItemTransform = hits.First().transform;
            return firstItemTransform == _transform;
        }
    }
}