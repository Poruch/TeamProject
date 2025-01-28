using UnityEngine;

namespace ApplicationSettings
{
    public class SettingManager
    {
        private SettingsConfig config;
        static SettingManager instance = new SettingManager();
        public static SettingManager Instance { get => instance; }
        public void SetConfig(SettingsConfig config)
        {
            this.config = config;

            SetFrameRate(config.Fps);
        }

        public void SetFrameRate(int fps)
        {
            if (fps < 30 || fps > 300) 
            {
                Debug.LogWarning("Fps is has not been change because not correct value");
                return;
            }
            Application.targetFrameRate = fps;
            config.Fps = fps;
        }
    }
}
