using Scripts.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Manager
{
    public class AudioManager : SimpleSingleton<AudioManager>, IManager
    {
        private const string BGM_GAMEOBJECT_NAME = "BGM_Audio";
        private const string SFX_GAMEOBJECT_NAME = "SFX_Audio";

        private const float SOUND_MIN_VOLUME = 0.0f;
        private const float SOUND_MAX_VOLUME = 1.0f;
        
        private const int SFX_POOL_SIZE = 5;

        private GameObject _audioManager;

        private AudioSource _bgmSource;
        private List<AudioSource> _sfxSources = new();

        // IManager
        public void Initialize()
        {
            _audioManager = this.gameObject;

            // BGM
            CreateBgm();
            
            // SFX
            CreateSfxPool();
        }
        public void Cleanup()
        {
        }
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        // Audio
        private void CreateBgm()
        {
            var bgm = Util.FindOrCreateGameObject(BGM_GAMEOBJECT_NAME);
            bgm.transform.parent = _audioManager.transform;

            _bgmSource = bgm.AddComponent<AudioSource>();
            _bgmSource.loop = true;
            _bgmSource.volume = 0.5f;
        }
        private void CreateSfxPool()
        {
            for (int i = 0; i < SFX_POOL_SIZE; i++)
            {
                var sfx = Util.FindOrCreateGameObject($"{SFX_GAMEOBJECT_NAME}_{i}");
                sfx.transform.parent = _audioManager.transform;

                var cmp = sfx.AddComponent<AudioSource>();
                cmp.loop = false;
                cmp.volume = 0.5f;

                _sfxSources.Add(cmp);
            }
        }


        public void PlayBGM(AudioClip clip)
        {
            if (clip == null)
                return;

            _bgmSource.clip = clip;
            _bgmSource.Play();
        }
        public void PlaySFX(AudioClip clip)
        {
            var source = _sfxSources.FirstOrDefault(s => !s.isPlaying);
            if (source != null && clip != null)
            {
                source.clip = clip;
                source.Play();
            }
        }


        public void StopBGM()
        {
            _bgmSource.Stop();
        }
        public void StopSFX()
        {
            foreach (var sfx in _sfxSources)
                sfx.Stop();
        }

        // UI 쪽에서 사용하면 될 듯 합니다.(설정창에서 사용할 듯)
        public void SetBgmVolume (float volume)
        {
            volume = Mathf.Clamp(volume, SOUND_MIN_VOLUME, SOUND_MAX_VOLUME);
            _bgmSource.volume = volume;
        }
        public void SetSfxVolume(float volume)
        {
            volume = Mathf.Clamp(volume, SOUND_MIN_VOLUME, SOUND_MAX_VOLUME);
            foreach (var sfx in _sfxSources)
                sfx.volume = volume;
        }

        public void MuteBgm()
        {
            _bgmSource.volume = SOUND_MIN_VOLUME;
        }
        public void MuteSfx()
        {
            foreach (var sfx in _sfxSources)
                sfx.volume = SOUND_MIN_VOLUME;
        }
    }
}