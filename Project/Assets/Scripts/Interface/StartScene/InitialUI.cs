using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void Init(LanguageConfig config)
    {
        settings.GetComponent<SettingUI>().Init();
        saves.GetComponent<SaveUI>().Init();
    }
    public void StartGame()
    {
        Saves = true;
        SceneManager.LoadScene("ScrollShooter");
    }

    public void OpenSettings() 
    {
        Settings = true;
    }

    public void OpendDefault()
    {
        DefaultUI = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
