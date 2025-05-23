using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GeneralGameUi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject pauseMenu;

    //������� ��� �������� � �������� ���������� �����
    UnityEvent onOpenUI = new UnityEvent();
    UnityEvent onCloseUI = new UnityEvent();

    [SerializeField]
    GameObject deathScreen;
    [SerializeField]
    GameObject winScreen;
    /// <summary>
    /// ��� �������� true ��������� ���������, ��� false ���������
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
        string commandText = @"Manual,_��������,_�������,_���������_���24_4.chm";
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
    public UnityEvent OnGameRestart { get => onGameRestart; set => onGameRestart = value; }
    public UnityEvent OnExit { get => onExit; set => onExit = value; }
    public UnityEvent OnOpenUI { get => onOpenUI; set => onOpenUI = value; }
    public UnityEvent OnCloseUI { get => onCloseUI; set => onCloseUI = value; }

    public void RestartGame()
    {
        OnGameRestart.Invoke();
    }

    public void ExitGame()
    {
        OnExit.Invoke();
    }
}
