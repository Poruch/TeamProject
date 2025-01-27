using TMPro;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsInput;
    public void OnCreate()
    {
        Application.targetFrameRate = 60;
    }

    public void Accept()
    {
        int fps = 60;
        if (!int.TryParse(fpsInput.text.Substring(0,fpsInput.text.Length-1), out fps))
            Debug.LogWarning("Неправильный формат ввода кадров в секунду " + fpsInput.text);
        Application.targetFrameRate = fps;
    }
}
