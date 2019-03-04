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

            if (!audioSource.isPlaying)
            {
                NextPhase();
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

    public void StartSong()
    {
        started = true;

        if (audioSource != null)
        {
            audioSource.clip = GetClipFromPhase(song, currentPhase);
            audioSource.Play();
        }

        currentSecondsBetweenBeats = normalSecondsBetweenBeats;
    }

    public void BeatCount()
    {
        timedBeatCounter++;
    }

    public void NextPhase()
    {
        timedBeatCounter = 0;

        if (currentPhase == "Intro")
        {
            playerController.ToggleLock(false);
            currentPhase = "Main";
        }
        else if (currentPhase == "Main")
        {
            if (bulletSpawner.batteries.Count > 0)
            {
                currentPhase = "Main";
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
        audioSource.Play();
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
        else if (phase == "Hyper")
        {
            return song.hyper;
        }
        else if (phase == "Charge")
        {
            return song.charge;
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
