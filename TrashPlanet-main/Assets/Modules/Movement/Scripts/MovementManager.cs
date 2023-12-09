using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GP1.Gameplay
{
    // Handles player speed, buffs and distance calculations
    public class MovementManager : MonoSingleton<MovementManager>
    {
        public float Speed { get; private set; }
        public float Multiplier { get; private set; }
        public float GlobalMultiplier
        {
            get => _globalMultiplier;
            set => _globalMultiplier = value;
        }
        public int DistanceGoal => _distanceGoal;
        public float TotalDistance { get; private set; }
        public int TotalUnits => Mathf.FloorToInt(TotalDistance);
        public LocationSO CurrentLocation => _locations[_currentLocationIndex];

        public UnityAction<int, float> UnitPassed;
        public UnityAction GoalReached;
        public UnityAction<int> LocationPassed;
        public UnityAction<int> LocationEntered;
        
        public int GetLastBuffCount(float duration) => _activeBuffs.Count(x => x.IsPositive && x.Time <= duration);

        public void AddBuff(MovementBuff buff)
        {
            _activeBuffs.Add(buff);
        }

        public void RemoveLastBuff()
        {
            if (_activeBuffs.Count > 0)
                _activeBuffs.RemoveAt(_activeBuffs.Count - 1);
        }

        private void Update()
        {
            UpdateBuffs();
            UpdateSpeed();
            UpdateDistance();
        }

        private void UpdateBuffs()
        {
            List<MovementBuff> buffsToRemove = new();
            _currentMultiplier = 1f;
            foreach (var buff in _activeBuffs)
            {
                if (buff.UpdateTime(Time.deltaTime))
                    buffsToRemove.Add(buff);
                else
                    _currentMultiplier += buff.GetCurrentMultiplier();
            }
            _activeBuffs.RemoveAll(x => buffsToRemove.Contains(x));
            _currentMultiplier = Mathf.Clamp(_currentMultiplier, _minMultiplier, 1e3f);
        }

        private void UpdateSpeed()
        {
            Multiplier = _currentMultiplier * _globalMultiplier;
            Speed = _baseSpeed * Multiplier;
        }
        
        private void UpdateDistance()
        {
            // Distance moved per current frame
            int totalUnits = TotalUnits;
            TotalDistance += Speed * Time.deltaTime;
            int newTotalUnits = TotalUnits;
            float offset = TotalDistance - newTotalUnits;
            // Update spawners per each unit moved
            for (int i = 0; i < newTotalUnits - totalUnits; i++)
            {
                UnitPassed?.Invoke(totalUnits, i + offset * 0f);
            }

            int DistanceWithinLocation = TotalUnits - CurrentLocation.Distance;
            if (DistanceWithinLocation > 0)
            {
                if (DistanceWithinLocation > CurrentLocation.Padding && _currentLocationIndex < _locations.Length - 1)
                {
                    _inTransition = false;
                    _currentLocationIndex++;
                    LocationEntered?.Invoke(_currentLocationIndex);
                }
                else if (!_inTransition)
                {
                    _inTransition = true;
                    LocationPassed?.Invoke(_currentLocationIndex);
                }
            }

            if (TotalUnits > _distanceGoal && !_goalReached)
            {
                GoalReached?.Invoke();
                _goalReached = true;
            }
        }

        private List<MovementBuff> _activeBuffs = new();
        private float _currentMultiplier;
        private float _globalMultiplier;
        private int _currentLocationIndex;
        private bool _inTransition;
        private bool _goalReached;

        [SerializeField]
        private int _distanceGoal;
        [Header("Speed")]
        [SerializeField]
        private float _baseSpeed;
        [SerializeField, Min(0)]
        private float _minMultiplier;
        [Header("Locations")]
        [SerializeField]
        private LocationSO[] _locations;
    }
}