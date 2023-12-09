using UnityEngine;

namespace GP1.Gameplay
{
    // Only show object with a random chance
    public class ObjectAppear : MonoBehaviour
    {
        private void Awake()
        {
            if (Random.value > _chance)
            {
                Destroy(gameObject);
            }
        }

        [SerializeField, Range(0, 1)]
        private float _chance;
    }
}
