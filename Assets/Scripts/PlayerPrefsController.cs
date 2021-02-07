using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{

    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string PLAYED_KEY = "played";
    const string WON_KEY = "won";
    const string LOST_KEY = "lost";
    const string DRAWN_KEY = "drawn";

    const float MIN_VALUE = 0f;
    const float MAX_VALUE = 1f;

    public static void SetMasterVolume(float volume) {
        if (volume >= MIN_VALUE && volume <= MAX_VALUE) {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        } else {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static float GetMasterVolume() {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 0.6f);
    }

    public static void SetDifficulty(int difficulty) {
        if (difficulty >= MIN_VALUE && difficulty <= MAX_VALUE) {
            PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
        } else {
            Debug.LogError("Difficulty is out of range");
        }
    }

    public static int GetDifficulty() {
        return PlayerPrefs.GetInt(DIFFICULTY_KEY, 0);
    }

    public static void SetPlayed() {
        int played = GetPlayed() + 1;
        PlayerPrefs.SetInt(PLAYED_KEY, played);
    }

    public static int GetPlayed() {
        return PlayerPrefs.GetInt(PLAYED_KEY, 0);
    }

    public static void SetWon() {
        int won = GetWon() + 1;
        PlayerPrefs.SetInt(WON_KEY, won);
    }

    public static int GetWon() {
        return PlayerPrefs.GetInt(WON_KEY, 0);
    }

    public static void SetLost() {
        int lost = GetLost() + 1;
        PlayerPrefs.SetInt(LOST_KEY, lost);
    }

    public static int GetLost() {
        return PlayerPrefs.GetInt(LOST_KEY, 0);
    }

    public static void SetDrawn() {
        int drawn = GetDrawn() + 1;
        PlayerPrefs.SetInt(DRAWN_KEY, drawn);
    }

    public static int GetDrawn() {
        return PlayerPrefs.GetInt(DRAWN_KEY, 0);
    }

    public static void ResetStats() {
        PlayerPrefs.SetInt(PLAYED_KEY, 0);
        PlayerPrefs.SetInt(WON_KEY, 0);
        PlayerPrefs.SetInt(LOST_KEY, 0);
        PlayerPrefs.SetInt(DRAWN_KEY, 0);
    }
}
