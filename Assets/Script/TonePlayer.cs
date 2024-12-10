using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TonePlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // 単音再生: C4
        AudioClip singleTone = AudioGenerator.GenerateTone(Tone.C, 4, 1.0f);
        audioSource.clip = singleTone;
        audioSource.Play();

        // コード再生: Cメジャー
        AudioClip chord = AudioGenerator.GenerateChord(Tone.C, ChordType.Major, 4, 1.0f);
        audioSource.clip = chord;
        audioSource.PlayDelayed(1.0f); // 単音再生後に再生
    }
}