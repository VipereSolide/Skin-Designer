using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using Core;

namespace SettingsMenu
{
    public class SettingsMenu : MonoBehaviour
    {
        public static SettingsMenu instance { get; private set; }

        public bool isEnabled = false;

        [Header("Resources")]

        public CanvasGroup canvasGroup;
        public ShortcutManager shortcutManager;

        private void RegisterSingleton()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            Destroy(this);
        }

        private void Awake()
        {
            RegisterSingleton();
        }

        private void Start()
        {
            UpdateGraphics();
        }

        public void Open()
        {
            isEnabled = true;
            UpdateGraphics();
        }

        public void Close()
        {
            isEnabled = false;
            UpdateGraphics();
        }

        private void Update()
        {
            if (isEnabled && shortcutManager.areShortcutsDisabled == false &&
                Input.GetKeyDown(shortcutManager.currentProfile.GetShortcutByName("Escape").combination[0]))
            {
                Close();
            }
        }

        public void UpdateGraphics()
        {
            canvasGroup.alpha = (isEnabled) ? 1 : 0;
            canvasGroup.interactable = isEnabled;
            canvasGroup.blocksRaycasts = isEnabled;
        }
    }
}