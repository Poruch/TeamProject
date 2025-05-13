using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GeneralGameUi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject pauseMenu;
    UnityEvent onPauseGame = new UnityEvent();
    UnityEvent onPauseExit = new UnityEvent();

    [SerializeField]
    GameObject deathScreen;
    public bool IsPause
    {
        get
        {
            return pauseMenu.activeSelf;
        }
        set
        {
            if (value)
            {
                OnPauseGame.Invoke();
            }
            else
            {
                OnPauseExit.Invoke();
            }
            pauseMenu.SetActive(value);
        }
    }
    public void OpenPauseMenu()
    {
        OnPauseGame.Invoke();
        pauseMenu.SetActive(true);
    }

    List<Button> pauseButtons;
    void Start()
    {
        pauseButtons = new List<Button>(pauseMenu.GetComponentsInChildren<Button>());

        pauseButtons[0].onClick.AddListener(RestartGame);
        pauseButtons[2].onClick.AddListener(ExitGame);
        pauseButtons[3].onClick.AddListener(OpenManual);

        pauseMenu.SetActive(false);
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        IsPause = true;
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
    public UnityEvent OnGameRestart { get => onGameRestart; set => onGameRestart = value; }
    public UnityEvent OnExit { get => onExit; set => onExit = value; }
    public UnityEvent OnPauseGame { get => onPauseGame; set => onPauseGame = value; }
    public UnityEvent OnPauseExit { get => onPauseExit; set => onPauseExit = value; }

    public void RestartGame()
    {
        OnGameRestart.Invoke();
    }

    public void ExitGame()
    {
        OnExit.Invoke();
    }
}
