namespace GP1.Gameplay
{
    // Runtime info about an active movement buff
    public class MovementBuff
    {
        public bool IsPositive => _data.Multiplier > 0;
        public float Time => _time;
        
        public bool UpdateTime(float delta)
        {
            _time += delta;
            return _time >= _data.Duration;
        }
        
        public float GetCurrentMultiplier() => _data.Multiplier * _data.FadeCurve.Evaluate(_time / _data.Duration);

        public MovementBuff(MovementBuffSO data)
        {
            _data = data;
            _time = 0f;
        }
        
        private float _time;
        private MovementBuffSO _data;
    }
}