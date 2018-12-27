using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    [HideInInspector] public Song song;

    public Song[] songsAvaliable;

    public delegate void Beat();
    public Beat beat;
    public Beat preBeat;
    public Beat postBeat;

    public int beatCounter = 0;

    [HideInInspector]
    public float secondsBetweenBeats;

    public bool currentlyInBeat;

    private AudioSource audioSource;

    private float beatTimer;
    private float lateBeatTimer;

    private float beatsUntilStart;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PickSong();

        if (audioSource != null)
        {
            audioSource.clip = song.audio;
            audioSource.Play();
        }

        secondsBetweenBeats = 60f / song.tempo;
        beatsUntilStart = song.barsUntilStart * song.beatsPerBar;
    }

    private void Update()
    {
        beatTimer -= Time.deltaTime;
        lateBeatTimer -= Time.deltaTime;

        if (beatTimer < 0.125f)
        {
            currentlyInBeat = true;

            if (preBeat != null)
            {
                preBeat.Invoke();
            }
        }
        if (beatTimer < 0)
        {
            currentlyInBeat = true;
            BeatCount();

            if (beat != null)
            {
                beat.Invoke();
            }
        }

        if (lateBeatTimer < -0.125f)
        {
            LateBeatCount();

            if (postBeat != null)
            {
                postBeat.Invoke();
            }
        }

        if (beatCounter > beatsUntilStart)
        {
            PlayerController.instance.ToggleLock(false);
        }
    }

    private void LateBeatCount()
    {
        currentlyInBeat = false;
        lateBeatTimer = beatTimer;
        PlayerController.instance.PlayerActedThisBeat = false;
    }

    private void PickSong()
    {
        int songIndex = Random.Range(0, songsAvaliable.Length);
        song = songsAvaliable[songIndex];
    }

    public void BeatCount()
    {
        beatTimer = secondsBetweenBeats;
        beatCounter++;
    }
}
