using System.Collections.Generic;
using UnityEngine;

namespace GP1.Gameplay
{
    // Move objects within the level
    public class LevelObjectManager : MonoBehaviour
    {
        public Transform ObjectParent => _objectParent;

        public void AddObject(Transform obj, float length)
        {
            obj.SetParent(_objectParent);
            _objects.Add(new LevelObject { Object = obj, HalfLength = length * 0.75f });
        }

        private void Start()
        {
            // Load initial location
            OnLocationEntered(_startLocation);

            // Load initial objects
            _objects = new();
            foreach (Transform child in _objectParent)
            {
                _objects.Add(new LevelObject { Object = child, HalfLength = 1f });
            }

            MovementManager.Instance.LocationPassed += OnLocationPassed;
            MovementManager.Instance.LocationEntered += OnLocationEntered;
        }

        private void LateUpdate()
        {
            UpdateObstacles();
        }

        private void UpdateObstacles()
        {
            List<Transform> obstaclesToRemove = new();
            float speed = MovementManager.Instance.Speed;
            foreach (var levelObject in _objects)
            {
                var obstacle = levelObject.Object;
                if (!obstacle)
                {
                    obstaclesToRemove.Add(obstacle);
                    continue;
                }
                obstacle.Translate(-Time.deltaTime * speed * Vector3.forward);
                var distance = -obstacle.localPosition.z;
                if (distance >= _cutoffDistance + levelObject.HalfLength)
                {
                    obstaclesToRemove.Add(obstacle);
                    Destroy(obstacle.gameObject);
                }
            }
            _objects.RemoveAll(x => obstaclesToRemove.Contains(x.Object));
        }

        private void RefreshSpawners()
        {
            var spawners = _spawnerParent.GetComponentsInChildren<ObjectSpawner>();
            foreach (var spawner in spawners)
                spawner.Setup(this);
        }

        private void LeaveLocation()
        {
            if (_currentLocation)
                Destroy(_currentLocation);
        }
        
        private void OnLocationPassed(int index)
        {
            LeaveLocation();
            var ramp = Instantiate(_transitionRamp.Prefab, _spawnerParent.transform.position + ObjectParent.forward * _transitionRamp.Length,
                ObjectParent.rotation).transform;
            ramp.localScale = ObjectParent.localScale;
            AddObject(ramp, _transitionRamp.Length);
        }

        private void OnLocationEntered(int index)
        {
            var newLocation = MovementManager.Instance.CurrentLocation.Prefab;
            _currentLocation = Instantiate(newLocation, _spawnerParent.transform.position, Quaternion.identity, _spawnerParent.transform);
            RefreshSpawners();
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(-Vector3.forward * _cutoffDistance, new Vector3(3f, 1f, 0.1f));
        }

        private struct LevelObject
        {
            public Transform Object;
            public float HalfLength;
        }
        
        private float _totalDistance;
        private List<LevelObject> _objects;
        private GameObject _currentLocation;

        [SerializeField]
        private GameObject _spawnerParent;
        [SerializeField]
        private Transform _objectParent;
        [SerializeField, Min(0)]
        private float _cutoffDistance;
        [SerializeField]
        private LevelObjectSO _transitionRamp;
        [SerializeField]
        private int _startLocation;
    }
}
