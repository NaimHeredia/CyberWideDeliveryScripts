using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script to play a particle for a set time
*/

public class PlayEffect : MonoBehaviour
{
    // Particle
    public ParticleSystem particle;

    // Time for the effect (can be set on inspector)
    float timer = 0.0f;

    // Function to play the partcile
    public void PlayParticle()
    {
        StartCoroutine(PlayParticleTimer(timer));
    }

    // Coroutine for the particle (allowing us to control the duration of the active particle)
    IEnumerator PlayParticleTimer(float time)
    {
        PlayParticle();
        yield return new WaitForSeconds(time);
        particle.Stop();
    }
}
