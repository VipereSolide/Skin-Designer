using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Shortcut Profile")]
    public class ShortcutProfile : ScriptableObject
    {
        public Shortcut[] shortcuts =
        {
            new Shortcut("Open Project", KeyCode.LeftControl, KeyCode.O),
            new Shortcut("Save Project", KeyCode.LeftControl, KeyCode.S),
            new Shortcut("Camera Pivot", KeyCode.Mouse1),
            new Shortcut("Camera Pan", KeyCode.Mouse2),
        };

        public Shortcut GetShortcutByName(string shortcutName)
        {
            for (int i = 0; i < shortcuts.Length; i++)
            {
                if (shortcuts[i].name == shortcutName)
                {
                    return shortcuts[i];
                }
            }

            Debug.LogError("Couldn't find any shortcut named " + shortcutName);
            return null;
        }
    }
}