using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace GP1.Gameplay
{
    // Handles spawning objects from the LevelObjectPoolSO
    public class ObjectSpawner : MonoBehaviour
    {
        public void Setup(LevelObjectManager manager) => _manager = manager;
        public void SetLastObject(LevelObjectSO obj) => _overrideLastObject = obj;

        public bool IsInPadding => _isInPadding;

        public UnityAction Spawned;

        private void OnEnable()
        {
            MovementManager.Instance.UnitPassed += OnUnitPassed;
        }
        
        private void OnDisable()
        {
            MovementManager.Instance.UnitPassed -= OnUnitPassed;
        }

        public void Spawn(int distanceTotal, float offset)
        {
            // Spawn the next object
            var nextObject = _allowRepeats ? _objectPool.GetRandom() : _objectPool.GetNext(_lastObject.LevelObject);
            if (_overrideLastObject)
                nextObject = _overrideLastObject;
            float horizontalOffset = Random.Range(-_width, _width) * 0.5f;
            Transform parent = _manager.ObjectParent;
            Vector3 spawnPoint = transform.position
                             + parent.forward * (offset + nextObject.Length * 0.5f)
                             + parent.right * horizontalOffset;
            var instance = Instantiate(nextObject.Prefab, spawnPoint, parent.rotation).transform;
            instance.localScale = transform.localScale;
            
            // Update last spawned object and padding
            _lastObject = new SpawnInfo { Distance = distanceTotal, LevelObject = nextObject };
            _padding = Random.Range(_paddingMin, _paddingMax + 1);
            
            // Add object to the manager
            _manager.AddObject(instance, nextObject.Length);
            Spawned?.Invoke();

            if (_overrideLastObject)
            {
                _overrideLastObject = null;
                gameObject.SetActive(false);
            }
        }

        public virtual bool CanSpawn(int totalDistance, float offset)
        {
            int dist = totalDistance - _lastObject.Distance;
            _isInPadding = dist >= _lastObject.LastLength * 0.5f;
            return Random.value < _chance && dist >= _lastObject.LastLength + _padding;
        }

        private void OnUnitPassed(int total, float offset)
        {
            if (CanSpawn(total, offset))
                Spawn(total, offset);
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = _color;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(_width, 1f, 0.1f));
        }

        private struct SpawnInfo
        {
            public int Distance;
            public LevelObjectSO LevelObject;

            public int LastLength => LevelObject?.Length ?? 0;
        }

        private bool _isInPadding;
        private LevelObjectManager _manager;
        private int _padding;
        private SpawnInfo _lastObject;
        private LevelObjectSO _overrideLastObject;
        
        [SerializeField]
        private LevelObjectPoolSO _objectPool;
        [SerializeField]
        private float _width;
        [Header("Spawn Logic")]
        [SerializeField]
        private bool _allowRepeats = true;
        [SerializeField]
        protected float _chance = 1f;
        [SerializeField]
        private int _paddingMin;
        [SerializeField]
        private int _paddingMax;
        [Header("Debug")]
        [SerializeField]
        private Color _color = Color.green;
    }
}