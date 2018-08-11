using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundSample { Error,Start,ToMain,ToSkin,ScrollSkin,Buy,SetSkin};

public class SoundManager : MonoBehaviour
{
    public static SoundManager sound_manager;
    AudioSource source;
    [SerializeField]
    AudioClip main_menu;
    [SerializeField]
    AudioClip game;

    [SerializeField]
    AudioClip error_sound;
    [SerializeField]
    AudioClip start_sound;
    [SerializeField]
    AudioClip toMain_sound;
    [SerializeField]
    AudioClip toSkin_sound;
    [SerializeField]
    AudioClip scrollSkin_sound;
    [SerializeField]
    AudioClip buy_sound;
    [SerializeField]
    AudioClip setSkin_sound;

    [SerializeField]
    AudioClip[] screams;

    float saved_vol=1.0f;

    void Awake()
    {
        sound_manager = this;
        source = GetComponent<AudioSource>();
        if (source.volume > 0.0f)
            saved_vol = source.volume;
    }

    public void On()
    {
        source.volume = saved_vol;
    }

    public void Off()
    {
        source.volume = 0.0f;
    }

    public void SingleSound(SoundSample sound)
    {
        switch (sound)
        {
            case SoundSample.Error:
                source.PlayOneShot(error_sound, 3.0f);
                break;
            case SoundSample.Start:
                source.PlayOneShot(start_sound, 3.0f);
                break;
            case SoundSample.ToMain:
                source.PlayOneShot(toMain_sound, 3.0f);
                break;
            case SoundSample.ToSkin:
                source.PlayOneShot(toSkin_sound, 3.0f);
                break;
            case SoundSample.ScrollSkin:
                source.PlayOneShot(scrollSkin_sound, 3.0f);
                break;
            case SoundSample.Buy:
                source.PlayOneShot(buy_sound, 3.0f);
                break;
            case SoundSample.SetSkin:
                source.PlayOneShot(setSkin_sound, 3.0f);
                break;
        }
    }

    public void Screamer()
    {
        source.PlayOneShot(screams[Random.Range(0,screams.Length)], 6.0f);
    }

    public void MainMenuTheme()
    {
        if (source.clip != main_menu)
        {
            source.clip = main_menu;
            source.Play();
        }
    }

    public void GameTheme()
    {
        if (source.clip!=game)
        {
            source.clip = game;
            source.Play();
        }
    }
}
