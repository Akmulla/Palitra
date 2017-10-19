using UnityEngine;

public enum SoundSample { Error,Start,ToMain,ToSkin,ScrollSkin,Buy,SetSkin}

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    AudioSource _source;
    [SerializeField]
    AudioClip _mainMenu;
    [SerializeField]
    AudioClip _game;

    [SerializeField]
    AudioClip _errorSound;
    [SerializeField]
    AudioClip _startSound;
    [SerializeField]
    AudioClip _toMainSound;
    [SerializeField]
    AudioClip _toSkinSound;
    [SerializeField]
    AudioClip _scrollSkinSound;
    [SerializeField]
    AudioClip _buySound;
    [SerializeField]
    AudioClip _setSkinSound;

    [SerializeField]
    AudioClip[] _screams;

    float _savedVol=1.0f;

    void Awake()
    {
        soundManager = this;
        _source = GetComponent<AudioSource>();
        if (_source.volume > 0.0f)
            _savedVol = _source.volume;
    }

    public void On()
    {
        _source.volume = _savedVol;
    }

    public void Off()
    {
        _source.volume = 0.0f;
    }

    public void SingleSound(SoundSample sound)
    {
        switch (sound)
        {
            case SoundSample.Error:
                _source.PlayOneShot(_errorSound, 3.0f);
                break;
            case SoundSample.Start:
                _source.PlayOneShot(_startSound, 3.0f);
                break;
            case SoundSample.ToMain:
                _source.PlayOneShot(_toMainSound, 3.0f);
                break;
            case SoundSample.ToSkin:
                _source.PlayOneShot(_toSkinSound, 3.0f);
                break;
            case SoundSample.ScrollSkin:
                _source.PlayOneShot(_scrollSkinSound, 3.0f);
                break;
            case SoundSample.Buy:
                _source.PlayOneShot(_buySound, 3.0f);
                break;
            case SoundSample.SetSkin:
                _source.PlayOneShot(_setSkinSound, 3.0f);
                break;
        }
    }

    public void Screamer()
    {
        _source.PlayOneShot(_screams[Random.Range(0,_screams.Length)], 6.0f);
    }

    public void MainMenuTheme()
    {
        if (_source.clip != _mainMenu)
        {
            _source.clip = _mainMenu;
            _source.Play();
        }
    }

    public void GameTheme()
    {
        if (_source.clip!=_game)
        {
            _source.clip = _game;
            _source.Play();
        }
    }
}
