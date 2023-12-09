using UnityEngine;

namespace GP1.Gameplay
{
    public class RandomRotate : MonoBehaviour
    {
        private void Awake()
        {
            var child = transform.GetChild(0);
            var angles = child.localEulerAngles;
            angles.y = Random.Range(_angleMin, _angleMax);
            child.localEulerAngles = angles;
        }

        [SerializeField]
        private float _angleMin = -180f;
        [SerializeField]
        private float _angleMax = 180f;
    }
}