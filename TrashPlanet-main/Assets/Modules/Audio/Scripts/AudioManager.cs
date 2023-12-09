using UnityEngine;

namespace GP1.Global
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public void PlaySound(AudioClip clip, float pitch, float volume)
        {
            _effectsSource.pitch = pitch;
            _effectsSource.volume = volume;
            _effectsSource.PlayOneShot(clip);
        }
        
        private AudioSource CreateSource(AudioClip clip, float pitch)
        {
            var source = new GameObject($"Oneshot: {clip.name}").AddComponent<AudioSource>();
            source.clip = clip;
            source.pitch = pitch;
            source.loop = false;
            return source;
        }

        [SerializeField]
        private AudioSource _musicSource;
        [SerializeField]
        private AudioSource _effectsSource;
    }
}