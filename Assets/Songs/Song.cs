using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public new string name = "New Song";

    [Space]
    public AudioClip intro;
    public float introBars = 8;
    [Space]

    [Space]
    public AudioClip main;
    public float mainBars = 8;
    [Space]

    [Space]
    public AudioClip transition;
    public float transitionBars = 8;
    [Space]

    [Space]
    public AudioClip hyper;
    public float hyperBars = 8;
    [Space]

    [Space]
    public AudioClip cooldown;
    public float cooldownBars = 8;
    [Space]

    [Space]
    public AudioClip chargeUp;
    public float chargeUpBars = 8;
    [Space]

    [Space]
    public AudioClip outro;
    public float outroBars = 8;
    [Space]

    public float tempo;
    public float beatsPerBar = 4;
}
