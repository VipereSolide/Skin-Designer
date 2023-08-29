using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine.EventSystems;
using UnityEngine;

using TMPro;
using Core;

namespace SettingsMenu
{
    public class ShortcutMenuShortcutObject : MonoBehaviour, IPointerDownHandler
    {
        public static readonly KeyCode[] CONTROL_KEYS =
        {
            KeyCode.LeftAlt,
            KeyCode.LeftApple,
            KeyCode.LeftCommand,
            KeyCode.LeftControl,
            KeyCode.LeftMeta,
            KeyCode.LeftShift,
            KeyCode.LeftWindows,
            KeyCode.RightAlt,
            KeyCode.RightApple,
            KeyCode.RightCommand,
            KeyCode.RightControl,
            KeyCode.RightMeta,
            KeyCode.RightShift,
            KeyCode.RightWindows,
        };

        public static bool IsControlKey(KeyCode keycode)
        {
            for(int i = 0; i < CONTROL_KEYS.Length; i++)
            {
                if (CONTROL_KEYS[i] == keycode)
                {
                    return true;
                }
            }

            return false;
        }

        public int shortcut;

        [Header("Resources")]

        public TMP_Text titleText;
        public TMP_Text shortcutText;
        public ShortcutManager shortcutManager;

        private bool isProcessing;
        private List<KeyCode> currentCombination;

        private float processTime;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isProcessing)
            {
                return;
            }

            isProcessing = true;
            processTime = Time.time + 0.15f;

            currentCombination = new List<KeyCode>();
            shortcutText.text = "_";
        }

        private void Update()
        {
            if (isProcessing == false || Time.time < processTime)
            {
                return;
            }

            shortcutManager.areShortcutsDisabled = true;

            foreach (string currentKeyName in Enum.GetNames(typeof(KeyCode)))
            {
                KeyCode key = Enum.Parse<KeyCode>(currentKeyName);

                if (!Input.GetKeyDown(key))
                {
                    continue;
                }

                if (currentCombination.Contains(key) == false)
                {
                    currentCombination.Add(key);
                }

                if (IsControlKey(key) == false)
                {
                    SetShortcut(currentCombination.ToArray());
                    break;
                }
            }
        }

        private void SetShortcut(KeyCode[] shortcut)
        {
            GetCurrentShortcut().combination = shortcut;
            
            isProcessing = false;
            currentCombination = null;
            shortcutManager.areShortcutsDisabled = false;

            UpdateUI();
        }

        public Shortcut GetCurrentShortcut()
        {
            return ShortcutManager.instance.currentProfile.shortcuts[shortcut];
        }

        public void UpdateUI()
        {
            shortcutText.text = GetCurrentShortcut().ToString();
            titleText.text = GetCurrentShortcut().name;
        }
    }
}
