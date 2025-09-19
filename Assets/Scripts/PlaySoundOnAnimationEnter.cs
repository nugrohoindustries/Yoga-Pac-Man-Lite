using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAnimationEnter : StateMachineBehaviour
{
    public AudioClip soundClip;
    public float volume = 1.0f;

    // Called when the animator enters the state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource audioSource = animator.GetComponent<AudioSource>();

        if (audioSource != null && soundClip != null)
        {
			audioSource.spatialBlend = 1f;
            audioSource.PlayOneShot(soundClip, volume);
        }
    }
}
