using System.Collections.Generic;
using System.Collections;

using UnityEngine;

using TMPro;

namespace Core
{
    public class FramerateCounter : MonoBehaviour
    {
        public float cooldown;
        public bool average = true;

        [Header("Resources")]

        public TMP_Text text;

        private float currentCooldown;

        private int sampleAmount;
        private float samples;

        public float GetFramerate()
        {
            return 1f / Time.deltaTime;
        }

        private void Update()
        {
            if (average)
            {
                samples += GetFramerate();
                sampleAmount++;
            }

            if (Time.time > currentCooldown)
            {
                currentCooldown = Time.time + cooldown;

                if (average)
                {
                    text.text = (Mathf.Floor((samples / sampleAmount) * 10) / 10).ToString() + " <size=65%>fps</size>";
                 
                    sampleAmount = 0;
                    samples = 0;
                }
                else
                {
                    text.text = GetFramerate().ToString() + " <size=65%>fps</size>";
                }
            }
        }
    }
}