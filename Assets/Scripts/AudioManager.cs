using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource introAudioSource;
    public AudioSource normalGhostsAudioSource;
    public AudioSource scaredGhostsAudioSource;
    public AudioSource deadGhostsAudioSource;
    
    void Start()
    {
        // Play intro music on game start
        introAudioSource.Play();
        // Wait for intro music to finish, then start normal ghost music
        StartCoroutine(PlayNormalGhostsMusicAfterIntro());
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
