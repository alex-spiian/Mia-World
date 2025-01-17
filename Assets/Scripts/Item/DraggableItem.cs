using System;
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

        private IInputHandler _inputHandler;

        private Vector3 _offset;
        private bool _isDragging;

        public void Initialize(IInputHandler inputHandler)
        {
            _mainCamera = Camera.main;
            _inputHandler = inputHandler;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_inputHandler == null)
                return;

            if (_inputHandler.IsPointerDown() && TryGetTopItemUnderPointer(out var topItem))
            {
                if (topItem == this)
                {
                    StartDragging(_inputHandler.GetPointerPosition());
                }
            }

            if (_isDragging)
            {
                if (_inputHandler.IsPointerDragging())
                {
                    DragItem(_inputHandler.GetPointerPosition());
                }

                if (_inputHandler.IsPointerUp())
                {
                    StopDragging();
                }
            }
        }

        private void StartDragging(Vector3 pointerPosition)
        {
            _offset = transform.position - GetWorldPosition(pointerPosition);
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            StartedDragging?.Invoke(this);
            _isDragging = true;
            SetSortingOrder(_maxSortingOrder);
        }

        private void DragItem(Vector3 pointerPosition)
        {
            transform.position = GetWorldPosition(pointerPosition) + _offset;
        }

        private void StopDragging()
        {
            _isDragging = false;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            SetSortingOrder(_minSortingOrder);
        }

        private Vector3 GetWorldPosition(Vector3 screenPosition)
        {
            var worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0;
            return worldPosition;
        }
        
        private bool TryGetTopItemUnderPointer(out DraggableItem topItem)
        {
            topItem = null;

            var pointerPosition = GetWorldPosition(_inputHandler.GetPointerPosition());
            var hits = Physics2D.RaycastAll(pointerPosition, Vector2.zero, 0f, _draggableLayerMask);

            if (hits.Length == 0)
                return false;

            var highestOrder = int.MinValue;
            foreach (var hit in hits)
            {
                var item = hit.collider.GetComponent<DraggableItem>();
                if (item != null)
                {
                    var order = item._spriteRenderer.sortingOrder;
                    if (order > highestOrder)
                    {
                        highestOrder = order;
                        topItem = item;
                    }
                }
            }

            return topItem != null;
        }

        public void SetSortingOrder(int order)
        {
            _spriteRenderer.sortingOrder = _minSortingOrder + order;
        }
    }
}