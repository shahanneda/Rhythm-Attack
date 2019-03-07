using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: FIX OFF-BEAT START ON NEW BPM.
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
    private int fastBeatCounter;

    private float extraTime = 0;

    [HideInInspector]
    public double currentSecondsBetweenBeats;
    public double normalSecondsBetweenBeats;
    public double fastSecondsBetweenBeats;

    public bool currentlyInBeat;

    public Animator beatAnim;
    public GameObject beatTick;

    public bool bossAlive = true;

    private AudioSource audioSource;

    private BulletSpawner bulletSpawner;

    private bool started;
    private bool postBeatInvoked;

    private string currentPhase = "Intro";

    private delegate void CheckRangeEvent();
    private CheckRangeEvent checkRangeEvent;

    private float startTime;
    private float endTime;

    private float elapsedTime = 0;
    private float elapsedNormalTime = 0;
    [SerializeField] private float elapsedFastTime = 0;

    [SerializeField]
    private PlayerController playerController;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        PickSong();

        checkRangeEvent += CheckNextPhase;

        bulletSpawner = FindObjectOfType<BulletSpawner>();
    }

    private void FixedUpdate()
    {
        if (started)
        {
            print(elapsedFastTime);
            elapsedTime = audioSource.time + extraTime;

            if (elapsedTime >= elapsedNormalTime + elapsedFastTime - 0.1d && elapsedTime < elapsedNormalTime + elapsedFastTime)
            {
                currentlyInBeat = true;
                postBeatInvoked = false;

                if (earlyBeat != null) earlyBeat.Invoke();
            }
            else if (elapsedTime >= elapsedNormalTime + elapsedFastTime && elapsedTime <= elapsedNormalTime + elapsedFastTime + 0.05d)
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
            else if (elapsedTime > normalSecondsBetweenBeats * (beatCounter - 1) + fastSecondsBetweenBeats * (fastBeatCounter - 1) && elapsedTime <= normalSecondsBetweenBeats * (beatCounter - 1) + fastSecondsBetweenBeats * (fastBeatCounter - 1) + 0.1d)
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

            CheckRange();
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

    private void AddExtraTime()
    {
        extraTime += endTime - startTime;
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

        SetRange(0f, song.mainStart);
    }

    public void BeatCount()
    {
        if (currentPhase == "Hyper")
        {
            fastBeatCounter++;
        }
        else
        {
            beatCounter++;
        }

        elapsedNormalTime = (float)normalSecondsBetweenBeats * beatCounter;
        elapsedFastTime = (float)fastSecondsBetweenBeats * fastBeatCounter;
    }

    public void SetRange(float startTime, float endTime)
    {
        this.startTime = startTime;
        this.endTime = endTime;
    }

    public void CheckRange()
    {
        if (audioSource.time >= endTime)
        {
            checkRangeEvent.Invoke();

            audioSource.time = startTime;
            audioSource.Play();
        }
    }

    public void CheckNextPhase()
    {
        if (currentPhase == "Intro")
        {
            PlayPhase("Main");
            playerController.ToggleLock(false);
        }
        else if (currentPhase == "Main")
        {
            if (bulletSpawner.batteries.Count <= 0)
            {
                PlayPhase("Hyper");
            }
            else
            {
                AddExtraTime();
            }
        }
        else if (currentPhase == "Hyper")
        {
            if (!bossAlive)
            {
                PlayPhase("Charge");
            }
            else
            {
                AddExtraTime();
            }
        }
        else if (currentPhase == "Charge")
        {
            PlayPhase("Outro");
            playerController.ToggleLock(true);
        }
    }

    public void PlayPhase(string phase)
    {
        currentPhase = phase;

        if (phase == "Intro")
        {
            SetRange(0f, song.mainStart);
        }
        else if (phase == "Main")
        {
            SetRange(song.mainStart, song.hyperStart);

            currentSecondsBetweenBeats = normalSecondsBetweenBeats;
        }
        else if (phase == "Hyper")
        {
            SetRange(song.hyperStart, song.chargeStart);

            currentSecondsBetweenBeats = fastSecondsBetweenBeats;
        }
        else if (phase == "Charge")
        {
            SetRange(song.chargeStart, song.outroStart);

            currentSecondsBetweenBeats = normalSecondsBetweenBeats;
        }
        else if (phase == "Outro")
        {
            SetRange(song.outroStart, song.full.length);
        }

        audioSource.Play();
    }
}
