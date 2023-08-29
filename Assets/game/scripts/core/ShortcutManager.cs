using System.Collections.Generic;
using System.Collections;

using UnityEngine;


namespace Core
{
    public class ShortcutManager : MonoBehaviour
    {
        public static ShortcutManager instance { get; private set; }

        public ShortcutProfile currentProfile;
        public bool areShortcutsDisabled = false;

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

        public static Shortcut GetShortcut(string shortcutName)
        {
            if (instance == null)
            {
                return new Shortcut("", KeyCode.None);
            }

            return instance.currentProfile.GetShortcutByName(shortcutName);
        }
    }
}