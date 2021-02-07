using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerText, thinkingText;
    [SerializeField] TextMeshProUGUI restartButton, menuButton;
    [SerializeField] Animator winAnimator;
    [SerializeField] float thinkTime = 0.5f;
    [SerializeField] float sfxVolume = 1f;
    [SerializeField] AudioClip tapSound, winSound, drawSound, loseSound;
    [SerializeField] List<TextMeshProUGUI> pointsTexts;

    bool myTurn;
    bool isEasy;
    string mySymbol;
    string cpuSymbol;
    List<int> pointsAvailable = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    List<int> myPoints;
    List<int> cpuPoints;

    void Start() {
        PlayerPrefsController.SetPlayed();
        isEasy = PlayerPrefsController.GetDifficulty() == 0;
        int coinToss = Random.Range(0, 2);
        myTurn = coinToss == 0;
        if (!myTurn) {
            mySymbol = "X";
            cpuSymbol = "O";
        } else {
            mySymbol = "O";
            cpuSymbol = "X";
        }
        myPoints = new List<int>();
        cpuPoints = new List<int>();
        ChangeTurn();
    }

    public void Play(int index) {
        if (!pointsAvailable.Contains(index) || !myTurn) {
            return;
        }
        pointsTexts[index].color = Color.blue;
        pointsTexts[index].text = mySymbol;
        pointsAvailable.Remove(index);
        myPoints.Add(index);
        PlayTypeSound();
        CheckWin();
    }

    private void ChangeTurn() {
        myTurn = !myTurn;
        if (myTurn) {
            playerText.text = "Your Turn";
            thinkingText.text = "";
        } else {
            playerText.text = "CPU Turn";
            thinkingText.text = "";
            StartCoroutine(CPUPlay());
        }
    }

    private void CheckWin() {
        bool win = false;
        List<int> points;
        if (myTurn) {
            points = myPoints;
        } else {
            points = cpuPoints;
        }
        // Win 1
        if (points.Contains(0) && points.Contains(3) && points.Contains(6)) {
            winAnimator.SetTrigger("Win 1");
            win = true;
        }
        // Win 2
        else if (points.Contains(1) && points.Contains(4) && points.Contains(7)) {
            winAnimator.SetTrigger("Win 2");
            win = true;
        }
        // Win 3
        else if (points.Contains(2) && points.Contains(5) && points.Contains(8)) {
            winAnimator.SetTrigger("Win 3");
            win = true;
        }
        // Win 4
        else if (points.Contains(0) && points.Contains(1) && points.Contains(2)) {
            winAnimator.SetTrigger("Win 4");
            win = true;
        }
        // Win 5
        else if (points.Contains(3) && points.Contains(4) && points.Contains(5)) {
            winAnimator.SetTrigger("Win 5");
            win = true;
        }
        // Win 6
        else if (points.Contains(6) && points.Contains(7) && points.Contains(8)) {
            winAnimator.SetTrigger("Win 6");
            win = true;
        }
        // Win 7
        else if (points.Contains(0) && points.Contains(4) && points.Contains(8)) {
            winAnimator.SetTrigger("Win 7");
            win = true;
        }
        // Win 8
        else if (points.Contains(2) && points.Contains(4) && points.Contains(6)) {
            winAnimator.SetTrigger("Win 8");
            win = true;
        }
        if (win) {
            EndGame(myTurn);
        } else {
            if (pointsAvailable.Count > 0) {
                ChangeTurn();
            } else {
                EndAsDraw();
            }
        }
    }

    private void EndGame(bool iWon) {
        myTurn = false;
        thinkingText.text = "";
        if (iWon) {
            PlayerPrefsController.SetWon();
            AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position, sfxVolume);
            playerText.text = "You WON!";
            playerText.color = Color.blue;
        } else {
            PlayerPrefsController.SetLost();
            AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position, sfxVolume);
            playerText.text = "You Lost...";
            playerText.color = Color.red;
        }
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    private void EndAsDraw() {
        myTurn = false;
        PlayerPrefsController.SetDrawn();
        AudioSource.PlayClipAtPoint(drawSound, Camera.main.transform.position, sfxVolume);
        thinkingText.text = "";
        playerText.text = "DRAW!";
        playerText.color = Color.grey;
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    private void PlayTypeSound() {
        AudioSource.PlayClipAtPoint(tapSound, Camera.main.transform.position, sfxVolume);
    }

    private IEnumerator CPUPlay() {
        yield return new WaitForSeconds(thinkTime);
        thinkingText.text = ".";
        yield return new WaitForSeconds(thinkTime);
        thinkingText.text = "..";
        yield return new WaitForSeconds(thinkTime);
        thinkingText.text = "...";
        yield return new WaitForSeconds(thinkTime);
        if (isEasy) {
            TryWin(GetRandomPick());
        } else {
            TryWin(HardMove());
        }
        PlayTypeSound();
        CheckWin();
    }

    private void TryWin(int randomPick) {
        // Win 1
        if (cpuPoints.Contains(0) && cpuPoints.Contains(3) || cpuPoints.Contains(0) && cpuPoints.Contains(6) || cpuPoints.Contains(3) && cpuPoints.Contains(6)) {
            var points = new List<int> { 0, 3, 6 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 2
        if (cpuPoints.Contains(1) && cpuPoints.Contains(4) || cpuPoints.Contains(1) && cpuPoints.Contains(7) || cpuPoints.Contains(4) && cpuPoints.Contains(7)) {
            var points = new List<int> { 1, 4, 7 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 3
        if (cpuPoints.Contains(2) && cpuPoints.Contains(5) || cpuPoints.Contains(2) && cpuPoints.Contains(8) || cpuPoints.Contains(5) && cpuPoints.Contains(8)) {
            var points = new List<int> { 2, 5, 8 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 4
        if (cpuPoints.Contains(0) && cpuPoints.Contains(1) || cpuPoints.Contains(0) && cpuPoints.Contains(2) || cpuPoints.Contains(1) && cpuPoints.Contains(2)) {
            var points = new List<int> { 0, 1, 2 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 5
        if (cpuPoints.Contains(3) && cpuPoints.Contains(4) || cpuPoints.Contains(3) && cpuPoints.Contains(5) || cpuPoints.Contains(4) && cpuPoints.Contains(5)) {
            var points = new List<int> { 3, 4, 5 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 6
        if (cpuPoints.Contains(6) && cpuPoints.Contains(7) || cpuPoints.Contains(6) && cpuPoints.Contains(8) || cpuPoints.Contains(7) && cpuPoints.Contains(8)) {
            var points = new List<int> { 6, 7, 8 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 7
        if (cpuPoints.Contains(0) && cpuPoints.Contains(4) || cpuPoints.Contains(0) && cpuPoints.Contains(8) || cpuPoints.Contains(4) && cpuPoints.Contains(8)) {
            var points = new List<int> { 0, 4, 8 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 8
        if (cpuPoints.Contains(2) && cpuPoints.Contains(4) || cpuPoints.Contains(2) && cpuPoints.Contains(6) || cpuPoints.Contains(4) && cpuPoints.Contains(6)) {
            var points = new List<int> { 2, 4, 6 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        pointsTexts[randomPick].color = Color.red;
        pointsTexts[randomPick].text = cpuSymbol;
        pointsAvailable.Remove(randomPick);
        cpuPoints.Add(randomPick);
    }

    private int HardMove() {
        int randomPick = GetRandomPick();
        // Win 1
        if (myPoints.Contains(0) && myPoints.Contains(3) || myPoints.Contains(0) && myPoints.Contains(6) || myPoints.Contains(3) && myPoints.Contains(6)) {
            var points = new List<int> { 0, 3, 6 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 2
        if (myPoints.Contains(1) && myPoints.Contains(4) || myPoints.Contains(1) && myPoints.Contains(7) || myPoints.Contains(4) && myPoints.Contains(7)) {
            var points = new List<int> { 1, 4, 7 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 3
        if (myPoints.Contains(2) && myPoints.Contains(5) || myPoints.Contains(2) && myPoints.Contains(8) || myPoints.Contains(5) && myPoints.Contains(8)) {
            var points = new List<int> { 2, 5, 8 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 4
        if (myPoints.Contains(0) && myPoints.Contains(1) || myPoints.Contains(0) && myPoints.Contains(2) || myPoints.Contains(1) && myPoints.Contains(2)) {
            var points = new List<int> { 0, 1, 2 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 5
        if (myPoints.Contains(3) && myPoints.Contains(4) || myPoints.Contains(3) && myPoints.Contains(5) || myPoints.Contains(4) && myPoints.Contains(5)) {
            var points = new List<int> { 3, 4, 5 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 6
        if (myPoints.Contains(6) && myPoints.Contains(7) || myPoints.Contains(6) && myPoints.Contains(8) || myPoints.Contains(7) && myPoints.Contains(8)) {
            var points = new List<int> { 6, 7, 8 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 7
        if (myPoints.Contains(0) && myPoints.Contains(4) || myPoints.Contains(0) && myPoints.Contains(8) || myPoints.Contains(4) && myPoints.Contains(8)) {
            var points = new List<int> { 0, 4, 8 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        // Win 8
        if (myPoints.Contains(2) && myPoints.Contains(4) || myPoints.Contains(2) && myPoints.Contains(6) || myPoints.Contains(4) && myPoints.Contains(6)) {
            var points = new List<int> { 2, 4, 6 };
            foreach (int i in points) {
                if (pointsAvailable.Contains(i)) {
                    randomPick = i;
                    break;
                }
            }
        }
        return randomPick;
    }

    private int GetRandomPick() {
        int randomPick;
        int randomIndex = Random.Range(0, pointsAvailable.Count);
        randomPick = pointsAvailable[randomIndex];
        return randomPick;
    }
}
