using System.Collections;
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

    private int beatCounter = 0;
    //private int lastBeatCounter;

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

    [SerializeField]
    private PlayerController playerController;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        PickSong();
    }

    private void FixedUpdate()
    {
        if (started)
        {
            if (audioSource.time >= currentSecondsBetweenBeats * beatCounter - 0.1d && audioSource.time < currentSecondsBetweenBeats * beatCounter)
            {
                currentlyInBeat = true;
                postBeatInvoked = false;

                if (earlyBeat != null) earlyBeat.Invoke();
            }
            else if (audioSource.time >= currentSecondsBetweenBeats * beatCounter && audioSource.time <= currentSecondsBetweenBeats * beatCounter + 0.05d)
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
            else if (audioSource.time > currentSecondsBetweenBeats * (beatCounter - 1) && audioSource.time <= currentSecondsBetweenBeats * (beatCounter - 1) + 0.1d)
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
            audioSource.clip = song.full;
            audioSource.Play();
        }

        currentSecondsBetweenBeats = normalSecondsBetweenBeats;
    }

    public void BeatCount()
    {
        beatCounter++;
    }

    public void CheckNextPhase()
    {
        if (currentPhase == "Intro")
        {
            if (audioSource.time >= song.mainStart)
            {
                PlayPhase("Main");
                playerController.ToggleLock(false);
            }
        }
        /*else if (currentPhase == "Main")
        {
            if (audioSource.time >= song.hyperStart)
            {
                PlayPhase("Hyper");
            }
        }
        else if (currentPhase == "Hyper")
        {
            if (audioSource.time >= song.chargeStart)
            {
                PlayPhase("Charge");
            }
        }
        else if (currentPhase == "Charge")
        {
            if (audioSource.time >= song.outroStart)
            {
                PlayPhase("Outro");
            }
        }*/
    }

    public void PlayPhase(string phase)
    {
        currentPhase = phase;

        if (phase == "Intro")
        {
            audioSource.time = 0;
        }
        else if (phase == "Main")
        {
            audioSource.SetScheduledStartTime(song.mainStart);
            audioSource.SetScheduledEndTime(song.hyperStart);

            currentSecondsBetweenBeats = normalSecondsBetweenBeats;
        }
        else if (phase == "Hyper")
        {
            audioSource.SetScheduledStartTime(song.hyperStart);
            audioSource.SetScheduledEndTime(song.chargeStart);

            currentSecondsBetweenBeats = fastSecondsBetweenBeats;
        }
        else if (phase == "Charge")
        {
            audioSource.SetScheduledStartTime(song.chargeStart);
            audioSource.SetScheduledEndTime(song.outroStart);

            currentSecondsBetweenBeats = normalSecondsBetweenBeats;
        }
        else if (phase == "Outro")
        {
            audioSource.SetScheduledStartTime(song.outroStart);
            audioSource.SetScheduledEndTime(song.full.length);
        }

        audioSource.Play();
    }
}
