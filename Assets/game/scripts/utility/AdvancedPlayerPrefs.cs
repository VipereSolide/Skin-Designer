using System.Collections.Generic;
using System.Collections;
using System.Text;

using UnityEngine;

namespace Utility
{
    public static class AdvancedPlayerPrefs
    {
        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) == 1;
        }

        public static void SetStringArray(string key, string[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                StringBuilder keyName = new StringBuilder();
                keyName.Append(key);
                keyName.Append("_");
                keyName.Append(i);

                PlayerPrefs.SetString(keyName.ToString(), value[i]);
            }
        }

        public static string[] GetStringArray(string key, int maxArraySize = 50)
        {
            List<string> array = new List<string>();

            for (int i = 0; i < maxArraySize; i++)
            {
                StringBuilder keyName = new StringBuilder();
                keyName.Append(key);
                keyName.Append("_");
                keyName.Append(i);

                if (PlayerPrefs.HasKey(keyName.ToString()))
                {
                    array.Add(PlayerPrefs.GetString(keyName.ToString()));
                }
                else
                {
                    break;
                }
            }

            return array.ToArray();
        }
    }
}