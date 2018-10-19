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
    public float secondsBetweenBeats;
    public int beatCounter = 0;

    private bool timing;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.clip = song.audio;
            //audioSource.Play();
        }

        beat += BeatCount;
        secondsBetweenBeats = 60f / song.tempo;

        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(20);

        audioSource.Play();
        GameController.instance.playerController.playerMovement.freeMove = false;
        timing = true;
    }

    private void FixedUpdate()
    {
        if (timing)
        {
            beatTimer -= Time.fixedDeltaTime;

            if (beatTimer < 0)
            {
                beat.Invoke();
            }
        }
    }

    public void BeatCount()
    {
        beatTimer = secondsBetweenBeats;
        beatCounter++;
        print("hi");
    }
}
