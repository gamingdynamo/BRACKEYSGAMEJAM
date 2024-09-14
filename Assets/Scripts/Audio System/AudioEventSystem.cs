using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Audio Event System",menuName ="Audio/Eventsystem")]
public class AudioEventSystem : ScriptableObject
{
    public void SetMusicVolume(float volume) => AudioSGT.Instance.SetMusicVolume(volume);
    public void SetGameVolume(float volume) => AudioSGT.Instance.SetGameVolume(volume);
    public void SetUIVolume(float volume) => AudioSGT.Instance.SetUIVolume(volume);
    public void SetAtmosVolume(float volume) => AudioSGT.Instance.SetAtmosVolume(volume);

    public void PlayUISound(AudioClip clip) => AudioSGT.Instance.PlayUIClip(clip);
    public void PlayMusic(AudioClip clip) => AudioSGT.Instance.PlayMusicClip(clip);
    public void PlayAtmos(AudioClip clip) => AudioSGT.Instance.PlayAtmosClip(clip);


}
