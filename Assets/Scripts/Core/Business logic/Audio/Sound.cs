using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Sound", menuName = "Sound/Create new sound", order = 0)]
public class Sound : ScriptableObject, ITableElement
{
    [field: SerializeField] public string Id { get; set; }
    [SerializeField] private AudioClip[] clips;
    [SerializeField] [Range(0f, 1f)] private float volume = 1.0f;
    [SerializeField] [Range(0f, 1.5f)] private float pitch = 1.0f;
    [SerializeField] private Vector2 randomVolumeRange = new Vector2(1.0f, 1.0f);
    [SerializeField] private Vector2 randomPitchRange = new Vector2(1.0f, 1.0f);
    [SerializeField] private bool inLoop = false;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    private AudioSource audioSource;

    /// <summary>
    /// Set the audio source of the sound.
    /// </summary>
    /// <param name="source">The audiosource.</param>
    public void SetSource(AudioSource source)
    {
        audioSource = source;
        int randomClip = Random.Range(0, clips.Length);
        audioSource.clip = clips[randomClip];
        audioSource.loop = inLoop;
        source.outputAudioMixerGroup = audioMixerGroup;
    }

    /// <summary>
    /// Play the sound.
    /// </summary>
    public void Play()
    {
        if (clips.Length > 1)
        {
            int randomClip = Random.Range(0, clips.Length);
            audioSource.clip = clips[randomClip];
        }
        audioSource.volume = volume * Random.Range(randomVolumeRange.x, randomVolumeRange.y);
        audioSource.pitch = pitch * Random.Range(randomPitchRange.x, randomPitchRange.y);
        audioSource.Play();
    }

    /// <summary>
    /// Stop the sound.
    /// </summary>
    public void Stop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Check if the sound is playing.
    /// </summary>
    /// <returns>Return the sound isPlaying value.</returns>
    public bool IsPlaying()
    {
        if (audioSource.isPlaying)
            return true;
        else
            return false;
    }
}
