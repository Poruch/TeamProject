using ApplicationSettings;
using UnityEngine;

public class StarterFirstScene : MonoBehaviour
{
    [SerializeField] GeneralConfig settings;
    [SerializeField] InitialUI initialUI;

    private void Awake()
    {
        SettingManager.Instance.SetConfig(settings.SettingsConfig);
        initialUI.Init(settings.LanguageConfig);
    }



}
