using System.Collections.Generic;
using System.Collections;
using System.IO;

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

            string path = Application.dataPath + "/data/";

            if (File.Exists(path + "shortcuts.json") == false)
            {
                currentProfile = new ShortcutProfile();
                return;
            }

            currentProfile = JsonUtility.FromJson<ShortcutProfile>(File.ReadAllText(path + "shortcuts.json"));

            ShortcutProfile defaultProfile = new ShortcutProfile();
            if (currentProfile.shortcuts.Count < defaultProfile.shortcuts.Count)
            {
                for (int i = 0; i < defaultProfile.shortcuts.Count; i++)
                {
                    if (currentProfile.GetShortcutByName(defaultProfile.shortcuts[i].name) == null)
                    {
                        currentProfile.shortcuts.Add(defaultProfile.shortcuts[i]);
                    }
                }
            }
        }

        private void OnApplicationQuit()
        {
            string path = Application.dataPath + "/data/";
            
            Directory.CreateDirectory(path);
            File.WriteAllText(path + "shortcuts.json", JsonUtility.ToJson(currentProfile));
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