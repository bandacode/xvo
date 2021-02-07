using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMenu() {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadOptions() {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadGame() {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadStats() {
        StartCoroutine(LoadLevel(3));
    }

    public void QuitGame() {
        StartCoroutine(Quit());
    }

    public IEnumerator Quit() {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Application.Quit();
    }

    IEnumerator LoadLevel(int index) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
}
