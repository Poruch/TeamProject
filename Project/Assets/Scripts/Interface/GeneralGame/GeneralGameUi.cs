using Assets.Scripts.GeneralGame;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Accessory;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using Assets.Scripts.Interface.GeneralGame;
using System.Diagnostics;

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
        loadScreen.gameObject.SetActive(false);
    }


    List<TextWriter> texts = new List<TextWriter>();
    [SerializeField]
    GameObject textInput;
    public void CreateTextOutput(Func<TextWriter, string> output,int time, Vector2 coords, Vector2 size)
    {
        GameObject gameObject = Instantiate(textInput, Vector2.zero,Quaternion.identity);
        gameObject.transform.SetParent(transform, false);
        gameObject.transform.SetSiblingIndex(0);
        RectTransform pos = gameObject.GetComponent<RectTransform>();
        pos.sizeDelta = size;
        pos.anchoredPosition = coords;
        var writer = gameObject.GetComponent<TextWriter>();
        writer.Instantiate(output,TimeManager.Instance.CreateTimer(time));
        texts.Add(writer);
    }

    //[SerializeField]
    //TextMeshProUGUI fps;
    //Timer timer = TimeManager.Instance.CreateTimer(1,true);
    //private void Update()
    //{
    //    if(timer.IsTime)
    //        fps.text = ((int)(1 / Time.deltaTime)).ToString();
    //}

    //[SerializeField]
    //TextMeshProUGUI waveTime;



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
        texts.ForEach(x => x.Disable());
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-150,0);
    }
    public void CloseDeathScreen()
    {
        deathScreen.SetActive(false);
        texts.ForEach(x => x.Enable());
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void OpenWinScreen()
    {
        winScreen.SetActive(true);
        texts.ForEach(x => x.Disable());
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -150, 0);
    }
    public void CloseWinScreen()
    {
        winScreen.SetActive(false);
        texts.ForEach(x => x.Enable());
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    
    public void StartLoadAnimation(bool right)
    {
        loadScreen.gameObject.SetActive(true);
        if (right)
            loadScreen.fillOrigin = (int)Image.OriginHorizontal.Right;
        else
            loadScreen.fillOrigin = (int)Image.OriginHorizontal.Left;
        loadScreen.fillAmount = 0;
        StartCoroutine(LoadAnimation(right));
    }
    IEnumerator LoadAnimation(bool right)
    {
        //float time = 0.1f;
        int countIteration = 100;
        for (int i = 0; i <= countIteration; i++)
        {
            if(right)
                loadScreen.fillAmount = (1 - (float)i / countIteration);
            else
                loadScreen.fillAmount = (float)i / countIteration;
            yield return null;//new WaitForSeconds(time / countIteration);
        }
        loadScreen.gameObject.SetActive(false);
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
