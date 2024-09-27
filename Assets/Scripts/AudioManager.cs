using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource introAudioSource;
    public AudioSource normalGhostsAudioSource;
    public AudioSource scaredGhostsAudioSource;
    public AudioSource deadGhostsAudioSource;
    private bool introPlaying = true;
    
    void Start()
    {
        // Play intro music
        introAudioSource.Play();
        StartCoroutine(PlayNormalGhostsMusicAfterIntro());
    }

    
    void Update()
    {
        // Skip intro if key pressed
        if (Input.anyKeyDown && introPlaying)
        {
            SkipIntro();
        }
    }
    
    void SkipIntro()
    {
        // Stop intro music
        introAudioSource.Stop();
        introPlaying = false;

        // Play normal ghost music
        normalGhostsAudioSource.Play();
    }
    
    IEnumerator PlayNormalGhostsMusicAfterIntro()
    {
        yield return new WaitForSeconds(introAudioSource.clip.length);
        normalGhostsAudioSource.Play();
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
