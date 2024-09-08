using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MusicChannelComponent : MonoBehaviour
{
    [SerializeField] private AudioSource channel1;
    [SerializeField] private AudioSource channel2;
    private UnityAction musicQueue;
    private bool isTransitioning;

    private void Update()
    {
        if (GetInvokationCount() <= 0 || isTransitioning)
            return;

        InvokeNextListener();
    }
    public void Play(AudioClip clip,float transitionTime)
    {
        musicQueue += () => StartCoroutine(PlayMusic(clip, transitionTime));
    }
    IEnumerator PlayMusic(AudioClip clip, float transitionTime)
    {
        isTransitioning = true;

        AudioSource nextChannel = channel1.isPlaying ? channel2 : channel1;
        AudioSource lastChannel = nextChannel == channel1 ? channel2 : channel1;

        nextChannel.clip = clip;
        nextChannel.Play();

        float timer = 0;

        /*
        string nextChannelName = nextChannel.clip == null ? "Nothing" : nextChannel.clip.name;
        string lastChannelName = lastChannel.clip == null ? "Nothing" : lastChannel.clip.name;
        Debug.Log("Transistioning From: " + lastChannelName + " To: " + nextChannelName);
        
         s*/
        while (timer < transitionTime)
        {
            float nextChannelVolume = Mathf.Lerp(0, 1, timer / transitionTime);
            float lastChannelVolume = Mathf.Lerp(1, 0, timer / transitionTime);

            nextChannel.volume = nextChannelVolume;
            lastChannel.volume = lastChannelVolume;

            timer += Time.deltaTime;
            yield return null;
        }

        lastChannel.Stop();
        isTransitioning = false;


    }

    public void InvokeNextListener()
    {
        if (musicQueue == null)
            return;

        UnityAction listener = musicQueue.GetInvocationList()[GetInvokationCount()-1] as UnityAction;

        if (listener != null)
        {
            listener.Invoke();
            musicQueue -= listener;
        }
    }

    public int GetInvokationCount()
    {
        if(musicQueue == null)
            return 0;

        return musicQueue.GetInvocationList().Length;
    }

}

