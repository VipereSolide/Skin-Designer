using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace Core
{
    public class ApplicationManager : MonoBehaviour
    {
        private int targetFPS = 60;

        public static readonly int FOCUS_TARGET_FPS = 60;
        public static readonly int UNFOCUSED_TARGET_FPS = 5;

        public void Exit()
        {
            Application.Quit();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                targetFPS = FOCUS_TARGET_FPS;
            }
            else
            {
                targetFPS = UNFOCUSED_TARGET_FPS;
            }

            UpdateTargetFPS();
        }

        private void UpdateTargetFPS()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFPS;
        }
    }
}