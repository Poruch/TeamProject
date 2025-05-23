using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GeneralGameUi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject pauseMenu;

    //События при открытии и закрытии интерфейса паузы
    UnityEvent onOpenUI = new UnityEvent();
    UnityEvent onCloseUI = new UnityEvent();

    UnityEvent onLoadAnimationEnd = new UnityEvent();

    [SerializeField]
    GameObject deathScreen;
    [SerializeField]
    GameObject winScreen;

    [SerializeField]
    Image loadScreen;
    /// <summary>
    /// При передаче true открывает интерфейс, при false закрывает
    /// </summary>
    public bool IsOpen
    {
        get
        {
            return pauseMenu.activeSelf;
        }
        set
        {
            if (value)
            {
                OnOpenUI.Invoke();
                OpenPauseMenu();
            }
            else
            {
                OnCloseUI.Invoke();
                ClosePauseMenu();
            }
        }
    }
    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    List<Button> pauseButtons;
    void Start()
    {
        pauseButtons = new List<Button>(pauseMenu.GetComponentsInChildren<Button>());

        pauseButtons[0].onClick.AddListener(RestartGame);
        pauseButtons[2].onClick.AddListener(ExitGame);
        pauseButtons[3].onClick.AddListener(OpenManual);

        CloseDeathScreen();
        ClosePauseMenu();
    }


    UnityEvent onGameRestart = new UnityEvent();
    UnityEvent onExit = new UnityEvent();
    public void OpenManual()
    {
        string commandText = @"Manual,_Катаргин,_Киладзе,_Мальшаков_РИС24_4.chm";
        var proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = commandText;
        proc.StartInfo.UseShellExecute = true;
        proc.Start();
        IsOpen = true;
    }
    public void OpenDeathScreen()
    {
        deathScreen.SetActive(true);
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-150,0);
    }
    public void CloseDeathScreen()
    {
        deathScreen.SetActive(false);
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void OpenWinScreen()
    {
        winScreen.SetActive(true);
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -150, 0);
    }
    public void CloseWinScreen()
    {
        winScreen.SetActive(false);
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    
    public void StartLoadAnimation(bool right)
    {
        if(right)
            loadScreen.fillOrigin = (int)Image.OriginHorizontal.Right;
        else
            loadScreen.fillOrigin = (int)Image.OriginHorizontal.Left;
        loadScreen.fillAmount = 0;
        StartCoroutine(LoadAnimation(right));
    }
    IEnumerator LoadAnimation(bool right)
    {
        float time = 0.1f;
        int countIteration = 60;
        for (int i = 0; i <= countIteration; i++)
        {
            if(right)
                loadScreen.fillAmount = (1 - (float)i / countIteration);
            else
                loadScreen.fillAmount = (float)i / countIteration;
            yield return null;//new WaitForSeconds(time / countIteration);
        }
        onLoadAnimationEnd.Invoke();
    }

    public UnityEvent OnGameRestart { get => onGameRestart; set => onGameRestart = value; }
    public UnityEvent OnExit { get => onExit; set => onExit = value; }
    public UnityEvent OnOpenUI { get => onOpenUI; set => onOpenUI = value; }
    public UnityEvent OnCloseUI { get => onCloseUI; set => onCloseUI = value; }
    public UnityEvent OnLoadAnimationEnd { get => onLoadAnimationEnd; set => onLoadAnimationEnd = value; }

    public void RestartGame()
    {
        OnGameRestart.Invoke();
    }

    public void ExitGame()
    {
        OnExit.Invoke();
    }
}
