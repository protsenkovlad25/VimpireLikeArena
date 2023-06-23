using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VampireLike.Core.Characters;

public class GameInterfaceManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_WinText;
    [SerializeField] private TMP_Text m_LoseText;
    [SerializeField] private TMP_Text m_SpawnChunkTimer;
    [SerializeField] private Image m_RedScreenImage;

    [SerializeField] private MainCharacter m_MainCharacter;

    private int initialHealth;
    private int currentHealth;

    private bool corotineChanging = false;

    private void Start()
    {
        EventManager.OnWin.AddListener(CompleteLevel);
        EventManager.OnLose.AddListener(LoseLevel);
        EventManager.MainCharacterTakeDamage.AddListener(RedScreenTransparencyChange);
        EventManager.OnInitEnemiesInAdvance.AddListener(TimerBlinking);

        initialHealth = m_MainCharacter.CharacterData.HealthPoints;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void CompleteLevel()
    {
        m_WinText.gameObject.SetActive(true);
        StartCoroutine(CompleteLevelPause());
    }

    public void LoseLevel()
    {
        m_LoseText.gameObject.SetActive(true);
        StartCoroutine(LoseLevelPause());
    }

    public void RedScreenTransparencyChange()
    {
        currentHealth = m_MainCharacter.CurrentHealthPoint;

        if ((currentHealth * 100) / initialHealth < 20)
        {
            Color color = new Color { r = 0.53f, g = 0f, b = 0f , a = 1f};
            
            m_RedScreenImage.DOColor(color, 0.5f);
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

    public void UpdateSpawnChunkTimer(int currentTime)
    {
        m_SpawnChunkTimer.text = currentTime.ToString();
    }

    public void ChunkTimerText(string text)
    {
        m_SpawnChunkTimer.text = text;
    }

    public void OffTimer()
    {
        m_SpawnChunkTimer.gameObject.SetActive(false);
    }

    public void OnTimer()
    {
        m_SpawnChunkTimer.gameObject.SetActive(true);
    }

    public void TimerBlinking()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(m_SpawnChunkTimer.DOColor(m_SpawnChunkTimer.color - new Color(0,0,0,1), 0.25f));
        sequence.Append(m_SpawnChunkTimer.DOColor(m_SpawnChunkTimer.color, 0.25f));

        sequence.SetLoops(3);
    }

    private IEnumerator CompleteLevelPause()
    {
        yield return new WaitForSeconds(3f);

        m_WinText.gameObject.SetActive(false);
        LoadMenuScene();
    }

    private IEnumerator LoseLevelPause()
    {
        yield return new WaitForSeconds(1f);

        m_LoseText.gameObject.SetActive(false);
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
            m_RedScreenImage.DOColor(color, 0.5f);

            yield return new WaitForSeconds(2f);
            m_RedScreenImage.DOFade(0, 0.5f);
        }
        else if (percentageHealth <= 79 && percentageHealth >= 60)
        {
            //RedScreenImage.DOFade(0.4f, 0.5f);
            color = new Color { r = 0.9f, g = 0.31f, b = 0.31f, a = 0.4f };
            m_RedScreenImage.DOColor(color, 0.5f);

            yield return new WaitForSeconds(2f);
            m_RedScreenImage.DOFade(0, 0.5f);
        }
        else if (percentageHealth <= 59 && percentageHealth >= 40)
        {
            //RedScreenImage.DOFade(0.6f, 0.5f);
            color = new Color { r = 0.82f, g = 0.2f, b = 0.2f, a = 0.6f };
            m_RedScreenImage.DOColor(color, 0.5f);

            yield return new WaitForSeconds(2f);
            m_RedScreenImage.DOFade(0, 0.5f);
        }
        else if (percentageHealth <= 39 && percentageHealth >= 20)
        {
            //RedScreenImage.DOFade(0.8f, 0.5f);
            color = new Color { r = 0.66f, g = 0.08f, b = 0.08f, a = 0.8f };
            m_RedScreenImage.DOColor(color, 0.5f);

            yield return new WaitForSeconds(2f);
            m_RedScreenImage.DOFade(0, 0.5f);
        }

        corotineChanging = false;
    }
}
