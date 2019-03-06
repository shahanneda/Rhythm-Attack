using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public new string name = "New Song";

    [Space]

    public AudioClip full;

    [Space]

    public float mainStart;
    public float hyperStart;
    public float outroStart;

    [Space]

    public float tempo;
    public float beatsPerBar = 4;
}
