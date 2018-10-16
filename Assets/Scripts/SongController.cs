using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public Song song;

    public delegate void Beat();
    public Beat beat;

    private AudioSource audioSource;

    private float beatTimer;
    private float secondsBetweenBeats;
    public int   beatCounter = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.clip = song.audio;
            audioSource.Play();
        }

        beat += BeatCount;
        secondsBetweenBeats = 60f / song.tempo;
    }

    private void FixedUpdate()
    {
        beatTimer -= Time.fixedDeltaTime;

        if (beatTimer < 0)
        {
            beat.Invoke();

        }
    }

    public void BeatCount()
    {
        beatTimer = secondsBetweenBeats;
        beatCounter++;
    }
}
