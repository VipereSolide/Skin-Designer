using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using Core;

namespace SettingsMenu
{
    public class ShortcutMenuList : MonoBehaviour
    {
        [Header("Resources")]

        public ShortcutMenuShortcutObject shortcutPrefab;
        public Transform shortcutsContainer;

        private void Start()
        {
            CreateShortcutPrefabs();
        }

        private void CreateShortcutPrefabs()
        {
            ShortcutManager manager = ShortcutManager.instance;
            ShortcutProfile profile = manager.currentProfile;

            for(int i = 0; i < profile.shortcuts.Length; i++)
            {
                ShortcutMenuShortcutObject shortcutObject = Instantiate(shortcutPrefab, shortcutsContainer);
                shortcutObject.shortcut = i;
                shortcutObject.shortcutManager = manager;
                shortcutObject.UpdateUI();
            }
        }
    }
}