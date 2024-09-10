using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]
public class SpatialSoundComponent : MonoBehaviour
{
    public GameObject Play(AudioClip clip, AudioMixerGroup group,Vector3 position)
    {
        transform.position = position;

        AudioSource source = GetComponent<AudioSource>();
        source.outputAudioMixerGroup = group;
        source.spatialBlend = 1;
        source.PlayOneShot(clip);
        StartCoroutine(DestroyOnClipEnd());

        IEnumerator DestroyOnClipEnd()
        {
            while (source.isPlaying)
                yield return null;
            Destroy(gameObject);
        }

        return this.gameObject;
    }
}
