using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VampireLike.Core.Characters;

public class GameInterfaceManager : MonoBehaviour
{
    [SerializeField] GameObject WinText;
    [SerializeField] GameObject LoseText;
    [SerializeField] Image RedScreenImage;

    [SerializeField] MainCharacter m_MainCharacter;

    private int initialHealth;
    private int currentHealth;

    private bool corotineChanging = false;

    private void Start()
    {
        EventManager.OnWin.AddListener(CompleteLevel);
        EventManager.OnLose.AddListener(LoseLevel);
        //EventManager.MainCharacterTakeDamage.AddListener(RedScreenTransparencyChange);

        initialHealth = m_MainCharacter.CharacterData.HealthPoints;
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

    public void RedScreenTransparencyChange()
    {
        currentHealth = m_MainCharacter.CurrentHealthPoint;

        if ((currentHealth * 100) / initialHealth < 20)
        {
            Color color = new Color { r = 0.53f, g = 0f, b = 0f , a = 1f};
            //RedScreenImage.DOFade(1, 0.5f);
            RedScreenImage.DOColor(color, 0.5f);
        }
        else
        {
            if (corotineChanging)
            {
                StopCoroutine("TemporaryRedScreen");
                StartCoroutine("TemporaryRedScreen");
            }
            else
            {
                corotineChanging = true;
                StartCoroutine("TemporaryRedScreen");
            }
        }
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

    private IEnumerator TemporaryRedScreen()
    {
        int percentageHealth = (currentHealth * 100) / initialHealth;
        Color color;

        if (percentageHealth <= 99 && percentageHealth >= 80)
        {
            //RedScreenImage.DOFade(0.2f, 0.5f);
            color = new Color { r = 1f, g = 0.39f, b = 0.39f, a = 0.2f };
            RedScreenImage.DOColor(color, 0.5f);

            yield return new WaitForSeconds(2f);
            RedScreenImage.DOFade(0, 0.5f);
        }
        else if (percentageHealth <= 79 && percentageHealth >= 60)
        {
            //RedScreenImage.DOFade(0.4f, 0.5f);
            color = new Color { r = 0.9f, g = 0.31f, b = 0.31f, a = 0.4f };
            RedScreenImage.DOColor(color, 0.5f);

            yield return new WaitForSeconds(2f);
            RedScreenImage.DOFade(0, 0.5f);
        }
        else if (percentageHealth <= 59 && percentageHealth >= 40)
        {
            //RedScreenImage.DOFade(0.6f, 0.5f);
            color = new Color { r = 0.82f, g = 0.2f, b = 0.2f, a = 0.6f };
            RedScreenImage.DOColor(color, 0.5f);

            yield return new WaitForSeconds(2f);
            RedScreenImage.DOFade(0, 0.5f);
        }
        else if (percentageHealth <= 39 && percentageHealth >= 20)
        {
            //RedScreenImage.DOFade(0.8f, 0.5f);
            color = new Color { r = 0.66f, g = 0.08f, b = 0.08f, a = 0.8f };
            RedScreenImage.DOColor(color, 0.5f);

            yield return new WaitForSeconds(2f);
            RedScreenImage.DOFade(0, 0.5f);
        }

        corotineChanging = false;
    }
}
