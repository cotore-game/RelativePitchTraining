using UnityEngine;

public class AudioGenerator : MonoBehaviour
{
    /// <summary>
    /// 単音のAudioClipを生成
    /// </summary>
    public static AudioClip GenerateTone(Tone tone, int octave, float length)
    {
        double frequency = ToneHelper.GetFrequency(tone, octave);
        return GenerateSineWave((float)frequency, length);
    }

    /// <summary>
    /// コードのAudioClipを生成
    /// </summary>
    public static AudioClip GenerateChord(Tone root, ChordType type, int octave, float length)
    {
        Tone[] tones = ChordHelper.GetChordTones(root, type);
        int sampleRate = 44100;
        int sampleCount = Mathf.CeilToInt(sampleRate * length);
        float[] data = new float[sampleCount];

        foreach (Tone tone in tones)
        {
            double frequency = ToneHelper.GetFrequency(tone, octave);
            for (int i = 0; i < sampleCount; i++)
            {
                data[i] += Mathf.Sin((float)(2 * Mathf.PI * frequency * i / sampleRate));
            }
        }

        // 正規化（クリッピング防止）
        float maxAmplitude = Mathf.Max(data);
        for (int i = 0; i < data.Length; i++)
        {
            data[i] /= maxAmplitude;
        }

        AudioClip clip = AudioClip.Create("Chord", sampleCount, 1, sampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }

    /// <summary>
    /// サイン波を生成
    /// </summary>
    private static AudioClip GenerateSineWave(float frequency, float length)
    {
        int sampleRate = 44100;
        int sampleCount = Mathf.CeilToInt(sampleRate * length);
        float[] data = new float[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            data[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate);
        }

        AudioClip clip = AudioClip.Create("SineWave", sampleCount, 1, sampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }
}