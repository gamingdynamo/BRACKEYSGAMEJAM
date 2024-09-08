using UnityEngine;
using UnityEngine.Audio;

public class AudioSGT : GenericSingleton<AudioSGT>
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerGroup mixerMaster;
    [SerializeField] private AudioMixerGroup mixerUI;
    [SerializeField] private AudioMixerGroup mixerMusic;
    [SerializeField] private AudioMixerGroup mixerGame;

    [Header("Audio Channel")]
    [SerializeField] private MusicChannelComponent channelMusic;
    [SerializeField] private AudioSource channelUI;

    private const float minVolume = -80f;
    private const float maxVolume = 0f;
    private const string VolumeAttribute = "Volume";

    private string MasterVolumeName => $"{mixerMaster.name} {VolumeAttribute}";
    private string GameVolumeName => $"{mixerGame.name} {VolumeAttribute}";
    private string MusicVolumeName => $"{mixerMusic.name} {VolumeAttribute}";
    private string UIVolumeName => $"{mixerUI.name} {VolumeAttribute}";

    public float GetMasterVolume() => mixer.GetFloat(MasterVolumeName, out float volume)? DecibelToNormalized(volume) : 0;
    public float GetGameVolume() => mixer.GetFloat(GameVolumeName, out float volume) ? DecibelToNormalized(volume) : 0;
    public float GetMusicVolume() => mixer.GetFloat(MusicVolumeName, out float volume) ? DecibelToNormalized(volume) : 0;
    public float GetUIVolume() => mixer.GetFloat(UIVolumeName, out float volume) ? DecibelToNormalized(volume) : 0;


    public void SetMasterVolume(float volume) => mixer.SetFloat(MasterVolumeName, NormalizedToDecibel(volume));
    public void SetGameVolume(float volume) => mixer.SetFloat(GameVolumeName, NormalizedToDecibel(volume));
    public void SetMusicVolume(float volume) => mixer.SetFloat(MusicVolumeName, NormalizedToDecibel(volume));
    public void SetUIVolume(float volume) => mixer.SetFloat(UIVolumeName, NormalizedToDecibel(volume));

    private float NormalizedToDecibel(float normalizedValue) => Mathf.Lerp(minVolume, maxVolume, Mathf.Clamp01(normalizedValue));
    private float DecibelToNormalized(float decibelValue) => Mathf.InverseLerp(minVolume,maxVolume, decibelValue);
    public void PlayUIClip(AudioClip clip) => channelUI.PlayOneShot(clip);
    public void PlayMusicClip(AudioClip clip, float transitionTime = 1) => channelMusic.Play(clip, transitionTime);

    public void PlayGameClip(AudioClip clip,Vector3 position) => _ = new GameObject(clip.name).AddComponent<SpatialSoundComponent>().Play(clip, mixerGame, position);
}