using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GP1.Gameplay
{
    // Destroy all children except a random one
    public class ObjectSingle : MonoBehaviour
    {
        private void Awake()
        {
            // Keep a single random child
            List<GameObject> children = new();
            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }

            int index = Random.Range(0, children.Count);
            for (int i = 0; i < children.Count; i++)
            {
                if (index != i)
                    Destroy(children[i]);
            }
        }
    }
}