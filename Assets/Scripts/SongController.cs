using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public Song song;

    public delegate void Beat();
    public Beat beat;

    private AudioSource audioSource;

    private float beatCounter;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.clip = song.audio;
            audioSource.Play();
        }

        beat += BeatCount;
    }

    private void FixedUpdate()
    {
        beatCounter -= Time.fixedDeltaTime;

        if (beatCounter < 0)
        {
            beat.Invoke();
        }
    }

    public void BeatCount()
    {
        beatCounter = 60f / song.tempo;
    }
}
