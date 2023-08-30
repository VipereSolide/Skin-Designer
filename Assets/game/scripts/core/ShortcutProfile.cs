using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace Core
{
    public class ShortcutProfile
    {
        public List<Shortcut> shortcuts = new List<Shortcut>
        {
            new Shortcut("Open Project", KeyCode.LeftControl, KeyCode.O),
            new Shortcut("Save Project", KeyCode.LeftControl, KeyCode.S),
            new Shortcut("Camera Pivot", KeyCode.Mouse1),
            new Shortcut("Camera Pan", KeyCode.Mouse2),
            new Shortcut("Escape", KeyCode.Escape),
            new Shortcut("Select Albedo Channel", KeyCode.A),
            new Shortcut("Select Metallic Channel", KeyCode.M),
            new Shortcut("Select Normal Channel", KeyCode.N),
            new Shortcut("Select Emission Channel", KeyCode.E),
            new Shortcut("Select Detail Channel", KeyCode.D),
            new Shortcut("Select Height Channel", KeyCode.H),
            new Shortcut("Select Occlusion Channel", KeyCode.O),
        };

        public Shortcut GetShortcutByName(string shortcutName)
        {
            for (int i = 0; i < shortcuts.Count; i++)
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