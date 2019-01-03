﻿using System.Collections;
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
    public double secondsBetweenBeats;

    public bool currentlyInBeat;

    public Animator beatAnim;
    public GameObject beatTick;

    private AudioSource audioSource;

    private double beatTimer;
    private double lateBeatTimer;

    private float beatsUntilStart;

    private bool started;

    private string currentPhase = "Intro";

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        PickSong();
    }

    private void FixedUpdate()
    {
        if (started)
        {
            beatTimer -= Time.deltaTime;
            lateBeatTimer -= Time.deltaTime;

            if (beatTimer < 0.225f)
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
                BeatAnim();

                Instantiate(beatTick, GameObject.Find("BeatCounter").transform).GetComponent<BeatTick>().direction = 1;
                Transform leftBeatTick = Instantiate(beatTick, GameObject.Find("BeatCounter").transform).transform;
                leftBeatTick.GetComponent<BeatTick>().direction = -1;
                leftBeatTick.position = new Vector2(Screen.width, leftBeatTick.position.y);

                if (beat != null)
                {
                    beat.Invoke();
                }
            }

            if (lateBeatTimer < -0.225f)
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

    private void BeatAnim()
    {
        beatAnim.Play("Beat");
    }

    public void StartSong()
    {
        started = true;

        if (audioSource != null)
        {
            audioSource.clip = GetClipFromPhase(song, currentPhase);
            audioSource.Play();
        }

        secondsBetweenBeats = 60f / song.tempo;
        beatsUntilStart = song.introBars * song.beatsPerBar;
    }

    public void BeatCount()
    {
        beatTimer = secondsBetweenBeats;
        beatCounter++;
    }

    public static AudioClip GetClipFromPhase(Song song, string phase)
    {
        if (phase == "Intro")
        {
            return song.intro;
        }
        else if (phase == "Main")
        {
            return song.main;
        }
        else if (phase == "Transition")
        {
            return song.transition;
        }
        else if (phase == "Hyper")
        {
            return song.hyper;
        }
        else if (phase == "Cooldown")
        {
            return song.cooldown;
        }
        else if (phase == "ChargeUp")
        {
            return song.chargeUp;
        }
        else if (phase == "Outro")
        {
            return song.outro;
        }
        else
        {
            return null;
        }
    }
}
