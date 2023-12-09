using UnityEngine;

namespace GP1.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Movement Buff")]
    public class MovementBuffSO : ScriptableObject
    {
        public float Duration => _duration;
        public float Multiplier => _multiplier;
        public AnimationCurve FadeCurve => _fadeOffCurve;

        public MovementBuff GetBuff() => new(this);

        [SerializeField]
        private float _multiplier;
        [SerializeField]
        private AnimationCurve _fadeOffCurve;
        [SerializeField, Min(0)]
        private float _duration;
    }
}