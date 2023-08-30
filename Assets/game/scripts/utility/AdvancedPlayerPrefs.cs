using System.Collections.Generic;
using System.Collections;
using System.Text;

using UnityEngine;

namespace Utility
{
    public static class AdvancedPlayerPrefs
    {
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
                    Debug.LogWarning($"Last found key was index {i}.");
                    break;
                }
            }

            return array.ToArray();
        }
    }
}