using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake() {
        SetUpSingleton();
    }

    private void SetUpSingleton() {
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = PlayerPrefsController.GetMasterVolume();
        }
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
