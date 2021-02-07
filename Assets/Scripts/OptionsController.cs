using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class OptionsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI volumeText, difficultyText, saveText;
    [SerializeField] float defaultVolume = 0.6f;
    [SerializeField] int defaultDifficulty = 0;

    float volume;
    int displayVolume, diff;

    void Start() {
        volume = PlayerPrefsController.GetMasterVolume();
        displayVolume = Convert.ToInt32(volume * 5);
        diff = PlayerPrefsController.GetDifficulty();
        DisplayDifficulty();
        DisplayVolume();
    }

    void Update() {
        var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer) {
            volume = displayVolume / 5f;
            musicPlayer.SetVolume(volume);
        }
    }

    public void SaveSettings() {
        volume = displayVolume / 5f;
        PlayerPrefsController.SetMasterVolume(volume);
        PlayerPrefsController.SetDifficulty(diff);
        StartCoroutine(ShowSaved());
    }

    private IEnumerator ShowSaved() {
        saveText.text = "SAVED";
        saveText.color = Color.green;
        yield return new WaitForSeconds(1f);
        saveText.text = "Save";
        saveText.color = Color.black;
    }

    public void SetDefaults() {
        volume = defaultVolume;
        diff = defaultDifficulty;
        DisplayDifficulty();
        DisplayVolume();
    }

    public void AddDifficulty() {
        if (diff >= 1) {
            diff = 1;
            DisplayDifficulty();
            return;
        }
        diff += 1;
        DisplayDifficulty();
    }

    public void SubtractDifficulty() {
        if (diff <= 0) {
            diff = 0;
            DisplayDifficulty();
            return;
        }
        diff -= 1;
        DisplayDifficulty();
    }

    public void AddVolume() {
        if (displayVolume >= 5) {
            displayVolume = 5;
            DisplayVolume();
            return;
        }
        displayVolume += 1;
        DisplayVolume();
    }

    public void SubtractVolume() {
        if (displayVolume <= 0) {
            displayVolume = 0;
            DisplayVolume();
            return;
        }
        displayVolume -= 1;
        DisplayVolume();
    }

    private void DisplayDifficulty() {
        if (diff == 0) {
            difficultyText.text = "Easy";
        } else {
            difficultyText.text = "Hard";
        }
    }

    private void DisplayVolume() {
        switch (displayVolume) {
            case 0:
                volumeText.text = "- - - - -";
                break;
            case 1:
                volumeText.text = "| - - - -";
                break;
            case 2:
                volumeText.text = "| | - - -";
                break;
            case 3:
                volumeText.text = "| | | - -";
                break;
            case 4:
                volumeText.text = "| | | | -";
                break;
            case 5:
                volumeText.text = "| | | | |";
                break;
        }
    }
}
