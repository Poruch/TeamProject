using UnityEngine;

public class InitialUI : MonoBehaviour 
{
    [SerializeField] GameObject saves;
    [SerializeField] GameObject defaultUI;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject backButton;

    GameObject CurrentActive;
    private bool DefaultUI
    {
        set
        {
            if (value)
            {
                defaultUI.SetActive(true);
                CurrentActive.SetActive(false);
                backButton.SetActive(false);
            }
            else
            {
                defaultUI.SetActive(false);
                backButton.SetActive(true);
            }
        }
    }
    private bool Saves
    {
        set
        {
            if (value)
            {
                saves.SetActive(true);
                CurrentActive = saves;
                DefaultUI = false;
            }
            else             
                DefaultUI = false;            
        }
    }
    private bool Settings
    {
        set
        {
            if (value)
            {
                settings.SetActive(true);
                CurrentActive = settings;
                DefaultUI = false;
            }
            else            
                DefaultUI = false;            
        }
    }
    private void Awake()
    {
        settings.GetComponent<SettingUI>().OnCreate();
    }
    public void StartGame()
    {
        Saves = true;        
    }

    public void OpenSettings() 
    {
        Settings = true;
    }

    public void OpendDefoult()
    {
        DefaultUI = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
