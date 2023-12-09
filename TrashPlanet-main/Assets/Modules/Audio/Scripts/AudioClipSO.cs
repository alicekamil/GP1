using System.Collections.Generic;
using GP1.Global;
using UnityEngine;

namespace GP1.Gameplay
{
    // Handles playing different AudioClips from the pool with extra options
    [CreateAssetMenu(menuName = "Audio/Clip")]
    public class AudioClipSO : ScriptableObject
    {
        public enum Type
        {
            Simple,
            Cycle,
            Unique
        }
        
        public virtual void Play(int overrideIndex = -1)
        {
            _lastClipIndex = overrideIndex == -1 ? GetNextClipIndex() : Mathf.Clamp(overrideIndex, 0, _clips.Length - 1);
            AudioManager.Instance.PlaySound(_clips[_lastClipIndex],
                Random.Range(_pitchMin, _pitchMax),
                _volume);
        }

        protected virtual int GetNextClipIndex()
        {
            switch (_type)
            {
                case Type.Simple:
                    return Random.Range(0, _clips.Length);
                case Type.Cycle:
                    return _lastClipIndex++;
                case Type.Unique:
                    List<int> available = new();
                    for (int i = 0; i < _clips.Length; i++)
                    {
                        if (i != _lastClipIndex)
                            available.Add(i);
                    }
                    return available[Random.Range(0, available.Count)];
            }

            return 0;
        }

        private int _lastClipIndex = -1;

        [SerializeField]
        protected AudioClip[] _clips;
        [SerializeField]
        private Type _type;
        [SerializeField, Min(0)]
        private float _volume = 1;
        [SerializeField, Min(0)]
        private float _pitchMin = 1;
        [SerializeField, Min(0)]
        private float _pitchMax = 1;
    }
}
