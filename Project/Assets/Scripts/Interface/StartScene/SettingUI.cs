using ApplicationSettings;
using TMPro;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsInput;
    [SerializeField] GeneralGameConfig config;

    public void Init()
    {

    }

    public void Accept()
    {
        int fps = 60;
        if (!int.TryParse(fpsInput.text.Substring(0,fpsInput.text.Length-1), out fps))
            Debug.LogWarning("������������ ������ ����� ������ � ������� " + fpsInput.text);
        SettingManager.Instance.SetFrameRate(fps);
    }


    public void Easy()
    {
        FloatingTextManager.Instance.CreateFloatingText("��������� ������", transform.position + Vector3.up, Color.green);
        config.DificultLevel = 0;
    }
    public void Middle()
    {
        FloatingTextManager.Instance.CreateFloatingText("��������� �������", transform.position , Color.blue);
        config.DificultLevel = 5;
    }
    public void Hard()
    {
        FloatingTextManager.Instance.CreateFloatingText("��������� �������", transform.position + Vector3.down, Color.red);
        config.DificultLevel = 10;
    }
}
