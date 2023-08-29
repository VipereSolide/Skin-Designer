using System.Collections.Generic;
using System.Collections;
using System.Text;

using UnityEngine;
using Utility;

namespace Core
{
    [System.Serializable]
    public class Shortcut
    {
        public string name;
        public KeyCode[] combination;

        public KeyCode ConvertKeyCode(KeyCode key)
        {
            if (key == KeyCode.RightAlt) return KeyCode.LeftAlt;
            if (key == KeyCode.RightApple) return KeyCode.LeftApple;
            if (key == KeyCode.RightCommand) return KeyCode.LeftCommand;
            if (key == KeyCode.RightControl) return KeyCode.LeftControl;
            if (key == KeyCode.RightShift) return KeyCode.LeftShift;
            if (key == KeyCode.RightWindows) return KeyCode.LeftWindows;

            return key;
        }

        public bool IsPressed()
        {
            if (combination == null || combination.Length == 0)
            {
                return false;
            }

            if (ShortcutManager.instance.areShortcutsDisabled)
            {
                return false;
            }

            for (int i = 0; i < combination.Length; i++)
            {
                if (Input.GetKey(ConvertKeyCode(combination[i])) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            if (combination == null || combination.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder text = new StringBuilder();

            foreach(KeyCode key in combination)
            {
                string treatedKey = key.ToString();
                treatedKey = treatedKey.ReplaceAll(new string[] { "LeftControl", "RightControl" }, "Ctrl");
                treatedKey = treatedKey.ReplaceAll(new string[] { "LeftShift", "RightShift" }, "Shift");
                treatedKey = treatedKey.ReplaceAll(new string[] { "LeftAlt", "RightAlt" }, "Alt");
                
                text.Append(treatedKey);

                if (key == combination[combination.Length - 1])
                {
                    break;
                }

                text.Append(" + ");
            }

            return text.ToString();
        }

        public Shortcut(string name, KeyCode key0)
        {
            this.name = name;
            combination = new KeyCode[] { key0 };
        }

        public Shortcut(string name, KeyCode key0, KeyCode key1)
        {
            this.name = name;
            combination = new KeyCode[] { key0, key1 };
        }

        public Shortcut(string name, KeyCode key0, KeyCode key1, KeyCode key2)
        {
            this.name = name;
            combination = new KeyCode[] { key0, key1, key2 };
        }

        public Shortcut(string name, KeyCode key0, KeyCode key1, KeyCode key2, KeyCode key3)
        {
            this.name = name;
            combination = new KeyCode[] { key0, key1, key2, key3 };
        }

        public Shortcut(string name, KeyCode key0, KeyCode[] keys)
        {
            this.name = name;
            combination = keys;
        }
    }
}