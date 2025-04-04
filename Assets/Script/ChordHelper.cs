public enum ChordType
{
    Major, Minor, Augmented, Diminished, Major7, Minor7, Dominant7
}

public static class ChordHelper
{
    /// <summary>
    /// コードの音階配列を生成
    /// </summary>
    public static Tone[] GetChordTones(Tone root, ChordType type)
    {
        switch (type)
        {
            case ChordType.Major:
                return new Tone[] { GetInterval(root, -12), root, GetInterval(root, 4), GetInterval(root, 7) };
            case ChordType.Minor:
                return new Tone[] { GetInterval(root, -12), root, GetInterval(root, 3), GetInterval(root, 7) };
            case ChordType.Augmented:
                return new Tone[] { GetInterval(root, -12), root, GetInterval(root, 4), GetInterval(root, 8) };
            case ChordType.Diminished:
                return new Tone[] { GetInterval(root, -12), root, GetInterval(root, 3), GetInterval(root, 6) };
            case ChordType.Major7:
                return new Tone[] { GetInterval(root, -12), root, GetInterval(root, 4), GetInterval(root, 7), GetInterval(root, 11) };
            case ChordType.Minor7:
                return new Tone[] { GetInterval(root, -12), root, GetInterval(root, 3), GetInterval(root, 7), GetInterval(root, 10) };
            case ChordType.Dominant7:
                return new Tone[] { GetInterval(root, -12), root, GetInterval(root, 4), GetInterval(root, 7), GetInterval(root, 10) };
            default:
                return new Tone[] { root };
        }
    }

    private static Tone GetInterval(Tone root, int semitones)
    {
        int index = ((int)root + semitones) % 12;
        return (Tone)index;
    }
}