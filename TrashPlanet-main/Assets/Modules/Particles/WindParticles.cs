using GP1.Gameplay;

using UnityEngine;

namespace GP1.Effects
{
    public class WindParticles : MonoBehaviour
    {
        private void Awake()
        {
            var particle = GetComponent<ParticleSystem>();
            _main = particle.main;
            _emission = particle.emission;
            _minSpeed = _main.startSpeed.constantMin;
            _maxSpeed = _main.startSpeed.constantMax;
            _rate = _emission.rateOverTime.constant;
        }

        private void Update()
        {
            float multiplier = MovementManager.Instance.Multiplier;
            _main.startSpeed = new ParticleSystem.MinMaxCurve(_minSpeed * multiplier, _maxSpeed * multiplier);
            _emission.rateOverTime = new ParticleSystem.MinMaxCurve(_rate * multiplier);
        }

        private ParticleSystem.MainModule _main;
        private ParticleSystem.EmissionModule _emission;
        private float _minSpeed;
        private float _maxSpeed;
        private float _rate;
    }
}
