using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    #endregion

    #region Fields

    private bool _currentMusicState;
    private bool _currentSfxState;
    private AudioClip _lastMusicClip;

    #endregion
        

    #region Monobehaviour

    private void Start()
    {
        _currentMusicState = true;
        _currentSfxState = true;
        StartCoroutine( CheckMusicCoroutine());
    }

    public void PlaySfxSound(AudioClip sound)
    {
        sfxSource.Stop();
        sfxSource.clip = sound;
        sfxSource.Play();
    }

    public void PlayMusic(AudioClip music)
    {
        _lastMusicClip = music;
        musicSource.Stop();
        musicSource.clip = music;
        musicSource.Play();
    }

    private IEnumerator CheckMusicCoroutine()
    {
        while (true)
        {
            if (!musicSource.isPlaying && _lastMusicClip != null)
            {
                musicSource.clip = _lastMusicClip;
                musicSource.Play();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void ToggleMusic()
    {
        audioMixer.SetFloat("MusicVolume", !_currentMusicState ? 0 : -80);
        _currentMusicState = !_currentMusicState;
    }

    public void ToggleSfx()
    {
        audioMixer.SetFloat("SfxVolume", !_currentSfxState ? 0 : -80);
        _currentSfxState = !_currentSfxState;
    }

    #endregion
}