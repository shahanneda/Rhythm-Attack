﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public Song song;

    public delegate void Beat();
    public Beat beat;
    public Beat preBeat;

    public int beatCounter = 0;

    [HideInInspector]
    public float secondsBetweenBeats;

    public bool currentlyInBeat;

    private AudioSource audioSource;

    private float beatTimer;

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

    private void Update()
    {
        beatTimer -= Time.deltaTime;

        if (beatTimer < 0.1f)
        {
            currentlyInBeat = true;
            preBeat.Invoke();
        }
        if (beatTimer < 0)
        {
            currentlyInBeat = true;
            beat.Invoke();
        }
    }

    public void BeatCount()
    {
        beatTimer = secondsBetweenBeats;
        beatCounter++;
        currentlyInBeat = false;
    }
}
