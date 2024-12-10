using System;

public enum Tone
{
    C, Db, D, Eb, E, F, Gb, G, Ab, A, Bb, B
}

public static class ToneHelper
{
    private const double BaseFrequency = 440.0; // A4 (基準周波数)
    private const int BaseOctave = 4; // A4のオクターブ

    /// <summary>
    /// 音階とオクターブから周波数を計算
    /// </summary>
    public static double GetFrequency(Tone tone, int octave)
    {
        int semitoneOffset = GetSemitoneOffset(tone, octave);
        return BaseFrequency * Math.Pow(2, semitoneOffset / 12.0);
    }

    /// <summary>
    /// 基準音 (A4) からの半音差を計算
    /// </summary>
    private static int GetSemitoneOffset(Tone tone, int octave)
    {
        int toneIndex = (int)tone;
        int baseIndex = (int)Tone.A; // A = 9
        int semitoneFromBase = toneIndex - baseIndex; // A基準の半音差
        int octaveOffset = (octave - BaseOctave) * 12; // オクターブ差
        return semitoneFromBase + octaveOffset;
    }
}