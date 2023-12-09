using UnityEngine;

namespace GP1.Gameplay
{
    [CreateAssetMenu(menuName = "Level/Location")]
    public class LocationSO : ScriptableObject
    {
        public string Name => _name;
        public GameObject Prefab => _prefab;
        public int Distance => _distance;
        public int Padding => _padding;
        
        [SerializeField]
        private string _name;
        [SerializeField]
        private int _distance;
        [SerializeField]
        private int _padding;
        [SerializeField]
        private GameObject _prefab;
    }
}