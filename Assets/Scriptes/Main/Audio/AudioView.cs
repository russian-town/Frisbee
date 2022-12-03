using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioView : MonoBehaviour
{
    public event UnityAction<bool> SoundButtonClicked;
    public event UnityAction<bool> MusicButtonClicked;

    [SerializeField] private Sprite _musicOn;
    [SerializeField] private Sprite _musicOff;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;

    private bool _isMusicPlay;
    private bool _isSoundPlay;

    private void OnEnable()
    {
        _musicButton.onClick.AddListener(OnMusicButtonClicked);
        _soundButton.onClick.AddListener(OnSoundButtonClicked);
    }

    private void OnDisable()
    {
        _musicButton.onClick.RemoveListener(OnMusicButtonClicked);
        _soundButton.onClick.RemoveListener(OnSoundButtonClicked);
    }

    public void Init(bool isMusicPlay, bool isSoundPlay)
    {
        if (isMusicPlay == true)
            _musicButton.image.sprite = _musicOn;
        else
            _musicButton.image.sprite = _musicOff;

        if(isSoundPlay == true)
            _soundButton.image.sprite = _soundOn;
        else
            _soundButton.image.sprite = _soundOff;
    }

    private void OnMusicButtonClicked()
    {
        if (_musicButton.image.sprite == _musicOn)
        {
            _musicButton.image.sprite = _musicOff;
            _isMusicPlay = false;
        }
        else
        {
            _musicButton.image.sprite = _musicOn;
            _isMusicPlay = true;
        }

        MusicButtonClicked?.Invoke(_isMusicPlay);
    }

    private void OnSoundButtonClicked()
    {
        if (_soundButton.image.sprite == _soundOn)
        {
            _soundButton.image.sprite = _soundOff;
            _isSoundPlay = false;
        }
        else
        {
            _soundButton.image.sprite = _soundOn;
            _isSoundPlay = true;
        }

        SoundButtonClicked?.Invoke(_isSoundPlay);
    }
}
