using System.Collections;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playedText, wonText, lostText, drawnText, resetText;

    int played, won, lost, drawn;

    // Start is called before the first frame update
    void Start()
    {
        played = PlayerPrefsController.GetPlayed();
        won = PlayerPrefsController.GetWon();
        lost = PlayerPrefsController.GetLost();
        drawn = PlayerPrefsController.GetDrawn();
        playedText.text = played.ToString();
        wonText.text = won.ToString();
        lostText.text = lost.ToString();
        drawnText.text = drawn.ToString();
    }

    public void ResetStats()
    {
        PlayerPrefsController.ResetStats();
        StartCoroutine(Reset());
    }

    private IEnumerator Reset() {
        resetText.text = "Done";
        resetText.color = Color.green;
        playedText.text = "0";
        wonText.text = "0";
        lostText.text = "0";
        drawnText.text = "0";
        yield return new WaitForSeconds(1f);
        resetText.text = "Reset";
        resetText.color = Color.black;
    }
}
