using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public AudioClip audio;
    public new string name = "New Song";
    public int tempo;
    public int beatsPerBar = 4;
}
