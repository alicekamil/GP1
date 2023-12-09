using System.Collections.Generic;
using UnityEngine;

namespace GP1.Gameplay
{
    [CreateAssetMenu(menuName = "Level/Object")]
    public class LevelObjectSO : ScriptableObject
    {
        public GameObject Prefab => _prefab;
        public int Length => _length;
        public IReadOnlyList<LevelObjectSO> IgnoreList => _ignoreList;

        [SerializeField]
        private GameObject _prefab;
        [SerializeField, Tooltip("Object length in units.")]
        private int _length;
        [SerializeField, Tooltip("Objects that can't be spawned after this one.")]
        private List<LevelObjectSO> _ignoreList;
    }
}
