using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
    Script to swap the music in the game based on collision with an invisible barrier in the game
    allowing us to control sections and show this to the player by sound
*/

public class SwapMusic : MonoBehaviour
{
    public AudioClip newMusic;
    private float transitionTime = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        // Make sure the only collision that triggers the music swap is the player
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayMusicWithFade(newMusic);
        }
    }
}
