using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TonePlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public float tonelength = 5.0f;
    public Tone playtone = Tone.C;
    [Range(0,7)] public int Pitch = 4;
    public ChordType playchordType = ChordType.Major;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        /*
        // 単音再生: C4
        AudioClip singleTone = AudioGenerator.GenerateTone(playtone, 4, tonelength);
        audioSource.clip = singleTone;
        audioSource.Play();
        */

        // コード再生: Cメジャー
        AudioClip chord = AudioGenerator.GenerateChord(playtone, playchordType, Pitch, tonelength);
        audioSource.clip = chord;
        audioSource.Play(); // 単音再生後に再生
    }
}