using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource normalGhostsAudioSource;
    public AudioSource scaredGhostsAudioSource;
    public AudioSource deadGhostsAudioSource;
    private bool introPlaying = true;
    
    void Start()
    {
        // Play intro music
        normalGhostsAudioSource.Play();
    }

    
    void Update()
    {
        if (Input.anyKeyDown && introPlaying)
        {
            
        }
    }

    // Call this method when ghosts are in scared state
    public void PlayScaredGhostsMusic()
    {
        normalGhostsAudioSource.Stop();
        scaredGhostsAudioSource.Play();
    }

    // Call this method when at least one ghost is dead
    public void PlayDeadGhostsMusic()
    {
        scaredGhostsAudioSource.Stop();
        deadGhostsAudioSource.Play();
    }
}
