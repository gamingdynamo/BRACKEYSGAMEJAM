using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorAutoDisable : MonoBehaviour
{
    private Animator anim;
    private void Awake() => anim = GetComponent<Animator>();
    private void Update() => anim.enabled = !FirstPersonController.Instance.controlledByLighthouse;
}
