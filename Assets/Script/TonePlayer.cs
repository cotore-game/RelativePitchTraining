using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TonePlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public float tonelength = 5.0f; // 長さ
    public TextMeshProUGUI chordText; // コード名
    public TMP_Dropdown toneDropdown, chordDropdown; // 選択肢
    public Button submitButton; // 提出ボタン
    public TextMeshProUGUI scoreText, timeText, resultText; // テキストUI
    public float gameTime = 30f; // 制限時間
    private float remainingTime;
    private int score = 0; // スコア
    private int streak = 0; // 連続正解によるボーナス
    private Tone correctTone; // 正解の音
    private ChordType correctChord; // 選択した音

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        remainingTime = gameTime;
        PopulateDropdown(toneDropdown, typeof(Tone));
        PopulateDropdown(chordDropdown, typeof(ChordType));
        GenerateRandomChord();
        submitButton.onClick.AddListener(CheckAnswer);
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;
        timeText.text = "Time: " + remainingTime.ToString("F1");
        if (remainingTime <= 0)
        {
            EndGame();
        }
    }

    private void PopulateDropdown(TMP_Dropdown dropdown, System.Type enumType)
    {
        dropdown.ClearOptions();
        List<string> options = new List<string>(System.Enum.GetNames(enumType));
        dropdown.AddOptions(options);
    }

    private void GenerateRandomChord()
    {
        correctTone = (Tone)Random.Range(0, 12);
        correctChord = (ChordType)Random.Range(0, 7);

        chordText.text = "Listen and Guess!";
        PlayChord(correctTone, correctChord);
    }

    private void PlayChord(Tone tone, ChordType chord)
    {
        AudioClip chordClip = AudioGenerator.GenerateChord(tone, chord, 4, tonelength);
        audioSource.clip = chordClip;
        audioSource.Play();
    }

    private void CheckAnswer()
    {
        Tone selectedTone = (Tone)toneDropdown.value;
        ChordType selectedChord = (ChordType)chordDropdown.value;

        string correctAnswer = GetChordName(correctChord, correctTone);
        chordText.text = "Correct Answer: " + correctAnswer;

        if (selectedTone == correctTone && selectedChord == correctChord)
        {
            streak++;
            score += 10 * streak;
            resultText.text = "Correct! +" + (10 * streak) + " points";
            StartCoroutine(NextQuestion(1f));
        }
        else
        {
            streak = 0;
            resultText.text = "Wrong!";
            StartCoroutine(NextQuestion(1.5f));
        }
        scoreText.text = "Score: " + score;
    }

    private IEnumerator NextQuestion(float interval)
    {
        yield return new WaitForSeconds(interval);
        GenerateRandomChord();
    }

    private void EndGame()
    {
        resultText.text = "Game Over! Final Score: " + score;
        submitButton.interactable = false;
    }

    private string GetChordName(ChordType playchordType, Tone playtone)
    {
        string toneStr = playtone.ToString();
        string chordStr = "";

        switch (playchordType)
        {
            case ChordType.Major:
                chordStr = "";
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
