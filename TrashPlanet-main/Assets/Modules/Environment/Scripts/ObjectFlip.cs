using UnityEngine;

namespace GP1.Gameplay
{
    // Randomly flip object's first child
    public class ObjectFlip : MonoBehaviour
    {
        private void Start()
        {
            // Only show up with a random chance
            if (Random.value > _chance)
            {
                var s = transform.localScale;
                s.x = -s.x;
                transform.localScale = s;
            }
        }

        [SerializeField, Range(0, 1)]
        private float _chance;
    }
}