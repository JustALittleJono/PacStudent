using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource normalGhostsAudioSource;
    public AudioSource scaredGhostsAudioSource;
    public AudioSource deadGhostsAudioSource;
    private bool isScared = false;
    private bool isDead = false;
    void Start()
    {
        // Play intro music
        normalGhostsAudioSource.Play();
    }

    
    void Update()
    {
        if (isScared is false && isDead is false)
        {
            PlayScaredGhostsMusic();
            isScared = true;
        }
        else if (isDead is true)
        {
            PlayDeadGhostsMusic();
            isScared = false;
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
        normalGhostsAudioSource.Stop();
        scaredGhostsAudioSource.Stop();
        deadGhostsAudioSource.Play();
    }
}
