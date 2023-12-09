using UnityEngine;
using Random = UnityEngine.Random;

namespace GP1.Gameplay
{
    // Spawn a random prefab from the bundle
    public class ObjectBundle : MonoBehaviour
    {
        private void Awake()
        {
            // Spawn a random prefab from the bundle
            Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], transform.position, transform.rotation, transform);
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            if (_preview)
            {
                Vector3 offset = _preview.transform.GetChild(0).localPosition;
                var mesh = _preview.GetComponentInChildren<MeshFilter>().sharedMesh;
                Gizmos.DrawWireMesh(mesh, offset);
                // Draw BBox instead
                // Gizmos.DrawWireCube(mesh.bounds.center + offset, mesh.bounds.size);
            }
        }

        [SerializeField]
        private GameObject _preview;
        [SerializeField]
        private GameObject[] _prefabs;
    }
}