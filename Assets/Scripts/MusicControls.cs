using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControls : MonoBehaviour
{
    public Image playPauseButton;
    public Sprite playSprite;
    public Sprite pauseSprite;

    public Text songText;
    public Text timeText;

    public Slider songSlider;

    private AudioSource musicSource;

    private float songLength;
    private string songLengthString;

    private void Start()
    {
        musicSource = FindObjectOfType<AudioSource>();
    }

    private void Update()
    {
        songSlider.value = musicSource.time / songLength;
        timeText.text = CalculateSongTime(musicSource.time) + "/" + songLengthString;
    }

    private string CalculateSongTime(float time)
    {
        return string.Format("{0:00}:{1:00}", Mathf.FloorToInt(time / 60f), Mathf.FloorToInt(time % 60f));
    }

    public void PlaySong(AudioClip clip)
    {
        songSlider.value = 0f;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();

        songLength = clip.length;
        songLengthString = CalculateSongTime(songLength);
    }

    public void UpdateSongText(string songName)
    {
        songText.text = songName;
    }

    public void PlayPause()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            playPauseButton.sprite = playSprite;
        }
        else
        {
            musicSource.Play();
            playPauseButton.sprite = pauseSprite;
        }
    }

    public void Scrub(float time)
    {
        musicSource.time = songLength * time;
    }
}
