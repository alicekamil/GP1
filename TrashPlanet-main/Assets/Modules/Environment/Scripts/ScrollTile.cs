using GP1.Gameplay;
using UnityEngine;

namespace GP1.Effects
{
    public class ScrollTile : MonoBehaviour
    {
        private void Awake()
        {
            _material = _rend.sharedMaterial;
        }

        private void Update()
        {
            float offset = MovementManager.Instance.TotalDistance / _scale;
            _material.mainTextureOffset = new Vector2(0, -offset);
        }

        private Material _material;
        
        [SerializeField]
        private MeshRenderer _rend;
        [SerializeField]
        private float _scale;
    }
}