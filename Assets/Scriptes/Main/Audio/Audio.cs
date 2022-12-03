using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioView))]
public class Audio : MonoBehaviour
{
    private const string SoundType = "Sound";
    private const string MusicType = "Music";
    private const string MusicVolumeParametr = "MusicVolume";
    private const string SoundVolumeParametr = "SoundVolume";
    private const float MinAudioVolume = -80f;
    private const float MaxAudioVolume = 0f;

    [SerializeField] private AudioMixerGroup _musicGroup;
    [SerializeField] private AudioMixerGroup _soundGroup;

    private AudioView _audioView;

    private void Awake()
    {
        _audioView = GetComponent<AudioView>();
    }

    private void OnEnable()
    {
        _audioView.SoundButtonClicked += OnSundButtonClicked;
        _audioView.MusicButtonClicked += OnMusicButtonClicked;
    }

    private void OnDisable()
    {
        _audioView.SoundButtonClicked -= OnSundButtonClicked;
        _audioView.MusicButtonClicked -= OnMusicButtonClicked;
    }

    private void Start()
    {
        OnMusicButtonClicked(Load(MusicType));
        OnSundButtonClicked(Load(SoundType));

        _audioView.Init(Load(MusicType), Load(SoundType));
    }

    private void OnSundButtonClicked(bool isSoundPlay)
    {
        if (isSoundPlay == true)
            _musicGroup.audioMixer.SetFloat(SoundVolumeParametr, MaxAudioVolume);
        else
            _musicGroup.audioMixer.SetFloat(SoundVolumeParametr, MinAudioVolume);

        Save(SoundType, isSoundPlay);
    }

    private void OnMusicButtonClicked(bool isMusicPlay)
    {
        if (isMusicPlay == true)
            _soundGroup.audioMixer.SetFloat(MusicVolumeParametr, MaxAudioVolume);
        else
            _soundGroup.audioMixer.SetFloat(MusicVolumeParametr, MinAudioVolume);

        Save(MusicType, isMusicPlay);
    }

    private void Save(string type, bool _isPlay)
    {
        PlayerPrefs.SetInt(type, _isPlay ? 1 : 0);
        PlayerPrefs.Save();
    }

    private bool Load(string type)
    {
        if (PlayerPrefs.HasKey(type))
           return PlayerPrefs.GetInt(type) == 1 ? true : false;

        return true;
    }
}
