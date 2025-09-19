using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundWithAnimation : MonoBehaviour
{
    public Animator animator;
    public string animationTriggerName = "Play";
    public AudioClip soundClip;

    private AudioSource audioSource;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAnimationWithSound()
    {
        // Play animation
        animator.SetTrigger(animationTriggerName);

        // Play sound
        if (soundClip != null)
            audioSource.PlayOneShot(soundClip);
    }
}
