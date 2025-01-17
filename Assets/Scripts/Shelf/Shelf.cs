using System.Collections.Generic;
using MiraWorld.Item;
using UnityEngine;

namespace MiraWorld.Shelf
{

    public class Shelf : MonoBehaviour
    {
        private readonly List<DraggableItem> _draggableItems = new();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<DraggableItem>(out var draggableItem))
            {
                _draggableItems.Add(draggableItem);
                SortItems();
                draggableItem.StartedDragging += OnItemLeft;
            }
        }

        private void OnItemLeft(DraggableItem draggableItem)
        {
            _draggableItems.Remove(draggableItem);
            SortItems();
            draggableItem.StartedDragging -= OnItemLeft;
        }

        private void SortItems()
        {
            var index = 0;
            foreach (var draggableItem in _draggableItems)
            {
                draggableItem.SetSortingOrder(index);
                index++;
            }
        }
    }
}