using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
    Audio Manager Script
    to control all audio, music, effects, swaps and control of the audio mixers
*/

public class AudioManager : MonoBehaviour
{
    // Audio Instance so we caN Access the audio manager from any script
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();                
            }
            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    // Variables
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private bool firstMusicPlaying;

    public AudioMixerGroup Music;
    public AudioMixerGroup Effects;

    public AudioMixer mainMixer;



    private void Awake()
    {
        // Dont destroy Instance on Load
        DontDestroyOnLoad(this.gameObject);

        // Create the Audio Sources 
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        // Set the Music to Loop
        musicSource.loop = true;
        musicSource2.loop = true;

        // Set the Mixer Groups
        musicSource.outputAudioMixerGroup = Music;
        musicSource2.outputAudioMixerGroup = Music;
        sfxSource.outputAudioMixerGroup = Effects;

    }

    public void PlayMusic(AudioClip musicClip)
    {
        // Check what audio source is active
        AudioSource activeMusicClip = (firstMusicPlaying) ? musicSource : musicSource2;

        activeMusicClip.clip = musicClip;
        activeMusicClip.volume = 0.4f;
        musicSource.Play();
    }

    // Fade the audio
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeMusicClip = (firstMusicPlaying) ? musicSource : musicSource2;

        StartCoroutine(UpdateMusicFade(activeMusicClip, newClip, transitionTime));
    }

    // Swap audio sources using the fade
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        AudioSource activeMusicClip = (firstMusicPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicPlaying) ? musicSource2 : musicSource;

        // Swap the sources
        firstMusicPlaying = !firstMusicPlaying;

        // Set to a new Audio Clip and start the Couroutine
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicCrossFade(activeMusicClip, newSource, transitionTime));
    }

    public IEnumerator UpdateMusicCrossFade(AudioSource fadedSource, AudioSource newSource, float transitionTime)
    {
        float t = 0.0f;

        for (t = 0; t <= transitionTime; t += Time.deltaTime)
        {
            fadedSource.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }
        fadedSource.Stop();
    }

    public IEnumerator UpdateMusicFade(AudioSource activeMusicClip, AudioClip newClip, float transitionTime)
    {
        // Check if there is an active source
        if (!activeMusicClip.isPlaying)
        {
            activeMusicClip.Play();
            //activeMusicClip.loop = false;
        }

        float t = 0.0f;

        // Fade Out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeMusicClip.volume = (1 - (t / transitionTime));
            yield return null;
        }

        activeMusicClip.Stop();
        activeMusicClip.clip = newClip;
        activeMusicClip.Play();

        // Fade In
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeMusicClip.volume = (t / transitionTime);
            yield return null;
        }
    }

    // Play and effect
    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }


    // For UI/Options Audio (volume can be set on the mixers from inspector using the sliders of the pause menu)

    public void SetMasterVolume(float volume)
    {
        mainMixer.SetFloat("Main", volume);        
    }

    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("Music", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        mainMixer.SetFloat("Effects", volume);
    }

}
