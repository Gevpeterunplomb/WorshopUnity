using Helteix.ChanneledProperties.Priorities;
using UnityEngine;

namespace WU
{
    public static class GameController
    {
        public static Priority<CursorLockMode> CursorLockModePriority { get; private set; }
        public static Priority<bool> CursorVisibleStatePriority { get; private set; }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadGame()
        {
            SetupPrioritisedProperties();
        }
        
        private static void SetupPrioritisedProperties()
        {
            CursorLockModePriority = new Priority<CursorLockMode>(CursorLockMode.None);
            CursorLockModePriority.AddOnValueChangeCallback(UpdateTimeScale, true);

            CursorVisibleStatePriority = new Priority<bool>(true);
            CursorVisibleStatePriority.AddOnValueChangeCallback(UpdateCursorVisibility, true);
        }
        
        private static void UpdateTimeScale(CursorLockMode value) => Cursor.lockState = value;
        private static void UpdateCursorVisibility(bool value) => Cursor.visible = value;
    }
}