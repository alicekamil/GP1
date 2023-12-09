using GP1.Global;
using UnityEngine;

namespace GP1.Gameplay
{
    public class BasePickup : MonoBehaviour
    {
        public void Pickup()
        {
            PlaySound();
            ParticleManager.Instance.Spawn(_particle, _effectPoint?.position ?? transform.position);
            OnPickup();
            Destroy(gameObject);
        }

        protected virtual void PlaySound()
        {
            _audioClip?.Play();
        }

        protected virtual void OnPickup() { }

        [SerializeField]
        protected AudioClipSO _audioClip;
        [SerializeField]
        private ParticleType _particle;
        [SerializeField]
        private Transform _effectPoint;
    }
}