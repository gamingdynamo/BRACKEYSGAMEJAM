using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footsteps = new List<AudioClip>();

    public void PlayFootstepSound()
    {
        if (footsteps.Count == 0)
            return;

        int rndIndex = Random.Range(0, footsteps.Count);
        AudioSGT.Instance.PlayGameClip(footsteps[rndIndex], transform.position);
    }
}
