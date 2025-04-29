using Scripts.Interface;
using UnityEngine;

namespace Scripts.Manager
{
    public class AudioManager : SimpleSingleton<AudioManager>, IManager
    {
        private GameObject _audioManager;

        private AudioSource _bgmSource;
        private AudioSource _fxSource;

        // IManager
        public void Initialize()
        {
            _bgmSource = gameObject.GetOrAddComponent<AudioSource>();
            _bgmSource.loop = true;

            _fxSource = gameObject.GetOrAddComponent<AudioSource>();
            _bgmSource.loop = true;
        }
        public void Cleanup()
        {
        }
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
        public void UpdateManager()
        {

        }

        // Audio
        public void PlayBGM(AudioClip clip)
        {
            if (clip == null)
                return;

            _bgmSource.clip = clip;
            _bgmSource.Play();
        }
        public void StopBGM()
        {
            _bgmSource.Stop();
        }
    }
}