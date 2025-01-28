using ApplicationSettings;
using DataManage;
using UnityEngine;

public class StarterFirstScene : MonoBehaviour
{
    [SerializeField] GeneralConfig settings;
    [SerializeField] InitialUI initialUI;

    private void Awake()
    {
        SaveManager.Instance.SetConfig(settings.SavesConfig);
        SettingManager.Instance.SetConfig(settings.SettingsConfig);
        initialUI.Init(settings.LanguageConfig);
    }



}
