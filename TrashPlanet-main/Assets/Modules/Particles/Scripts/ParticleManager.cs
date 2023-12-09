using System;
using UnityEngine;

namespace GP1.Global
{
    public enum ParticleType
    {
        None,
        SmallPickup,
        Crash,
        BigPickup
    }
    
    public class ParticleManager : MonoSingleton<ParticleManager>
    {
        public void Spawn(ParticleType type, Vector3 position, Transform overrideParent = null)
        {
            var prefab = GetParticleObject(type);
            if (prefab)
                Instantiate(prefab, position, Quaternion.identity, overrideParent);
        }
        
        private GameObject GetParticleObject(ParticleType type) => type switch
        {
            ParticleType.None => null,
            ParticleType.SmallPickup => _smallPickup,
            ParticleType.Crash => _crash,
            ParticleType.BigPickup => _bigPickup,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"VfxType not found: {type}"),
        };
        
        [Header("Particles")]
        [SerializeField]
        private GameObject _smallPickup;
        [SerializeField]
        private GameObject _bigPickup;
        [SerializeField]
        private GameObject _crash;
    }
}