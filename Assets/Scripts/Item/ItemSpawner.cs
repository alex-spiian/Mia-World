using MiraWorld.InputHandler;
using UnityEngine;

namespace MiraWorld.Item
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private DraggableItem _draggableItemPrefab;
        [SerializeField] private int _maxItemsCount;
        [SerializeField] private Transform[] _spawnPoints;
        
        private IInputHandler _inputHandler;

        public void Initialize(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public void Spawn()
        {
            var spawnPointIndex = 0;
            for (int i = 0; i < _maxItemsCount; i++)
            {
                var item = Instantiate(_draggableItemPrefab);
                item.Initialize(_inputHandler);

                item.transform.position = GetSpawnPosition(ref spawnPointIndex);
                spawnPointIndex++;
            }
        }

        private Vector3 GetSpawnPosition(ref int spawnPointIndex)
        {
            if (spawnPointIndex >= _spawnPoints.Length)
            {
                spawnPointIndex = 0;
            }

            return _spawnPoints[spawnPointIndex].position;
        }
    }
}