using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public int levelMusicToPlay;

    public AudioSource[] music;
    public AudioSource[] sfx;

    public AudioMixerGroup musicMixer, sfxMixer;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PlayMusic(2);
    }

    void Update()
    {

    }

    public void PlayMusic(int musicToPlay)
    {
        music[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }

    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.Instance.musicVolSlider.value);
    }

    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SfxVol", UIManager.Instance.sfxVolSlider.value);
    }
}
