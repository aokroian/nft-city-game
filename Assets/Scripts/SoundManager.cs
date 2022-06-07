using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    #endregion
        

    #region Monobehaviour

    public void PlaySfxSound(AudioClip sound)
    {
        sfxSource.Stop();
        sfxSource.clip = sound;
        sfxSource.Play();
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.Stop();
        musicSource.clip = music;
        musicSource.Play();
    }


    public void MuteMusix(bool mute)
    {
        audioMixer.SetFloat("MusicVolume", mute ? 0 : -80);
    }

    public void MuteSfx(bool mute)
    {
        audioMixer.SetFloat("SfxVolume", mute ? 0 : -80);
    }
        
    #endregion
}