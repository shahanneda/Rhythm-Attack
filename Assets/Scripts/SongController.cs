﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: MAKE SONG ONLY 1 CLIP AND MEASURE TIME OF DIFFERENT PHASES (TO MAKE CONTINOUS)
public class SongController : MonoBehaviour
{
    [HideInInspector] public Song song;

    public Song[] songsAvaliable;

    public delegate void Beat();
    public Beat beat;
    public Beat earlyBeat;
    public Beat lateBeat;
    public Beat postBeat;

    private int timedBeatCounter = 0;

    [HideInInspector]
    public double currentSecondsBetweenBeats;
    public double normalSecondsBetweenBeats;
    public double fastSecondsBetweenBeats;

    public bool currentlyInBeat;

    public Animator beatAnim;
    public GameObject beatTick;

    public bool bossAlive = true;

    private AudioSource audioSource;

    private bool started;
    private bool postBeatInvoked;

    private string currentPhase = "Intro";

    private BulletSpawner bulletSpawner;

    [SerializeField]
    private PlayerController playerController;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        PickSong();

        bulletSpawner = FindObjectOfType<BulletSpawner>();
    }

    private void FixedUpdate()
    {
        if (started)
        {
            if (audioSource.time >= currentSecondsBetweenBeats * timedBeatCounter - 0.1d && audioSource.time < currentSecondsBetweenBeats * timedBeatCounter)
            {
                currentlyInBeat = true;
                postBeatInvoked = false;

                if (earlyBeat != null) earlyBeat.Invoke();
            }
            else if (audioSource.time >= currentSecondsBetweenBeats * timedBeatCounter && audioSource.time <= currentSecondsBetweenBeats * timedBeatCounter + 0.05d)
            {
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
            else if (audioSource.time > currentSecondsBetweenBeats * (timedBeatCounter - 1) && audioSource.time <= currentSecondsBetweenBeats * (timedBeatCounter - 1) + 0.1d)
            {
                if (lateBeat != null) lateBeat.Invoke();
            }
            else
            {
                if (!postBeatInvoked)
                {
                    postBeatInvoked = true;
                    postBeat.Invoke();
                    PostBeat();
                }
            }

            CheckNextPhase();
        }
    }

    private void PostBeat()
    {
        currentlyInBeat = false;
        playerController.PlayerActedThisBeat = false;
    }

    private void PickSong()
    {
        int songIndex = Random.Range(0, songsAvaliable.Length);
        song = songsAvaliable[songIndex];

        normalSecondsBetweenBeats = 60d / song.tempo;
        fastSecondsBetweenBeats = 40d / song.tempo;
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
            audioSource.clip = /*GetClipFromPhase(song, currentPhase)*/ song.full;
            audioSource.Play();
        }

        currentSecondsBetweenBeats = normalSecondsBetweenBeats;
    }

    public void BeatCount()
    {
        timedBeatCounter++;
    }

    public void CheckNextPhase()
    {
        timedBeatCounter = 0;

        /*if (currentPhase == "Intro")
        {
            playerController.ToggleLock(false);
            currentPhase = "Main";
        }
        else if (currentPhase == "Main")
        {
            if (bulletSpawner.batteries.Count > 0)
            {
                currentPhase = "Main";
                currentSecondsBetweenBeats = normalSecondsBetweenBeats;
            }
            else
            {
                currentPhase = "Hyper";
                currentSecondsBetweenBeats = fastSecondsBetweenBeats;
            }
        }
        else if (currentPhase == "Hyper")
        {
            currentPhase = "Charge";
        }
        else if (currentPhase == "Charge")
        {
            if (bossAlive)
            {
                currentPhase = "Hyper";
            }
            else
            {
                currentPhase = "Outro";
                currentSecondsBetweenBeats = normalSecondsBetweenBeats;

                playerController.ToggleLock(true);
            }
        }
        else if (currentPhase == "Outro")
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("Menu");
        }

        audioSource.clip = GetClipFromPhase(song, currentPhase);
        audioSource.Play();*/

        if (currentPhase == "Intro")
        {
            if (audioSource.time >= song.mainStart)
            {
                PlayPhase("Main");
                playerController.ToggleLock(false);
            }
        }
        else if (currentPhase == "Main")
        {
            if (audioSource.time >= song.hyperStart)
            {
                PlayPhase("Hyper");
            }
        }
        else if (currentPhase == "Hyper")
        {
            if (audioSource.time >= song.outroStart)
            {
                PlayPhase("Outro");
            }
        }
    }

    public void PlayPhase(string phase)
    {
        currentPhase = phase;
        print(phase);

        if (phase == "Intro")
        {
            audioSource.time = 0;
        }
        else if (phase == "Main")
        {
            audioSource.time = song.mainStart;
            currentSecondsBetweenBeats = normalSecondsBetweenBeats;
        }
        else if (phase == "Hyper")
        {
            audioSource.time = song.hyperStart;
            currentSecondsBetweenBeats = fastSecondsBetweenBeats;
        }
        else if (phase == "Outro")
        {
            audioSource.time = song.outroStart;
        }
    }
}
