using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public AudioClip audio;
    public new string name = "New Song";
    public float tempo;
    public float beatsPerBar = 4;
    public float barsUntilStart = 8;
}
