using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class TonePlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public float tonelength = 5.0f;
    public Tone playtone = Tone.C;
    [Range(0, 7)] public int Pitch = 4;
    public ChordType playchordType = ChordType.Major;
    public TextMeshProUGUI chordText;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateChordText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayChord();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopChord();
        }
    }

    private void PlayChord()
    {
        UpdateChordText();

        AudioClip chord = AudioGenerator.GenerateChord(playtone, playchordType, Pitch, tonelength);
        audioSource.clip = chord;
        audioSource.Play();
    }

    private void StopChord()
    {
        audioSource.Stop();
    }

    private void UpdateChordText()
    {
        string chordName = GetChordName();
        chordText.text = chordName;
    }

    private string GetChordName()
    {
        string toneStr = playtone.ToString();
        string chordStr = "";

        switch (playchordType)
        {
            case ChordType.Major:
                chordStr = "M";
                break;
            case ChordType.Minor:
                chordStr = "m";
                break;
            case ChordType.Augmented:
                chordStr = "aug";
                break;
            case ChordType.Diminished:
                chordStr = "dim";
                break;
            case ChordType.Major7:
                chordStr = "M7";
                break;
            case ChordType.Minor7:
                chordStr = "m7";
                break;
            case ChordType.Dominant7:
                chordStr = "7";
                break;
        }

        return $"{toneStr}<size=110>{chordStr}</size>";
    }
}
