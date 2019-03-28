using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongController : MonoBehaviour
{
    [HideInInspector] public Song song;

    public Song[] songsAvaliable;

    public delegate void Beat();
    public Beat beat;
    public Beat earlyBeat;
    public Beat lateBeat;
    public Beat postBeat;

    private float beatCounter = 0;
    private float fastBeatCounter;

    [SerializeField] private float extraTime = 0;

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
    private bool ended = false;

    private bool postBeatInvoked;

    private string currentPhase = "Intro";
    public string CurrentPhase { get { return currentPhase; } }

    private float startTime;
    private float endTime;

    [SerializeField] private float elapsedTime = 0;
    private float elapsedNormalTime = 0;
    private float elapsedFastTime = 0;

    [SerializeField] private PlayerController playerController;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        PickSong();

        bulletSpawner = FindObjectOfType<BulletSpawner>();
    }

    private void Update()
    {
        if (ended)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Stop();
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            if (started)
            {
                elapsedTime = audioSource.time + extraTime;
                elapsedNormalTime = (float)normalSecondsBetweenBeats * beatCounter;
                elapsedFastTime = (float)fastSecondsBetweenBeats * fastBeatCounter;

                CheckRange();

                if (elapsedTime >= elapsedNormalTime + elapsedFastTime - 0.3d + 0.04d && elapsedTime < elapsedNormalTime + elapsedFastTime + 0.04d)
                {
                    currentlyInBeat = true;
                    postBeatInvoked = false;

                    if (earlyBeat != null) earlyBeat.Invoke();
                }
                else if (elapsedTime >= elapsedNormalTime + elapsedFastTime + 0.04d /*&& elapsedTime <= elapsedNormalTime + elapsedFastTime + 0.05d + 0.033d*/)
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
                else if (elapsedTime > normalSecondsBetweenBeats * (beatCounter - 1) + fastSecondsBetweenBeats * (fastBeatCounter - 1) + 0.04d && elapsedTime <= normalSecondsBetweenBeats * (beatCounter - 1) + fastSecondsBetweenBeats * (fastBeatCounter - 1) + 0.3d + 0.04d)
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
            }
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

    private void AddExtraTimeHyperLoop()
    {
        float hyperTime = song.chargeStart - song.hyperStart;
        float chargeTime = song.outroStart - song.chargeStart;

        extraTime += hyperTime + chargeTime;
    }

    private void CheckRange()
    {
        if (audioSource.time >= endTime)
        {
            if (!NextPhase())
            {
                audioSource.time = startTime;
                audioSource.Play();
            }
        }
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
    }

    public void SetRange(float startTime, float endTime)
    {
        this.startTime = startTime;
        this.endTime = endTime;
    }

    public bool NextPhase()
    {
        bool nextPhase = true;

        if (currentPhase == "Intro")
        {
            PlayPhase("Main");
            playerController.ToggleLock(false);
        }
        else if (currentPhase == "Main")
        {
            if (bulletSpawner.batteries.Count <= 0)
            {
                extraTime -= song.hyperStart - audioSource.time;

                PlayPhase("Hyper");
            }
            else
            {
                AddExtraTime();
                nextPhase = false;
            }
        }
        else if (currentPhase == "Hyper")
        {
            PlayPhase("Charge");
        }
        else if (currentPhase == "Charge")
        {
            if (!bossAlive)
            {
                PlayPhase("Outro");
                playerController.ToggleLock(true);
                playerController.playerMovement.MovePlayerTo(Vector2.zero);
                ended = true;
            }
            else
            {
                AddExtraTimeHyperLoop();
                PlayPhase("Hyper");
            }
        }

        return nextPhase;
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

        audioSource.time = startTime;
        audioSource.Play();
    }
}
