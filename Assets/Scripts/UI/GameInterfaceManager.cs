using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInterfaceManager : MonoBehaviour
{
    [SerializeField] GameObject WinText;
    [SerializeField] GameObject LoseText;

    private void Start()
    {
        EventManager.OnWin.AddListener(CompleteLevel);
        EventManager.OnLose.AddListener(LoseLevel);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void CompleteLevel()
    {
        WinText.SetActive(true);
        StartCoroutine(CompleteLevelPause());
    }

    public void LoseLevel()
    {
        LoseText.SetActive(true);
        StartCoroutine(LoseLevelPause());
    }

    private IEnumerator CompleteLevelPause()
    {
        yield return new WaitForSeconds(3f);
        
        WinText.SetActive(false);
        LoadMenuScene();
    }

    private IEnumerator LoseLevelPause()
    {
        yield return new WaitForSeconds(1f);

        LoseText.SetActive(false);
        LoadMenuScene();
    }
}
