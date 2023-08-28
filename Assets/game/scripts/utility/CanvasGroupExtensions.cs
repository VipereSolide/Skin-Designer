using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace Utility
{
    public static class CanvasGroupExtensions
    {
        public static void SetActive(this CanvasGroup canvasGroup, bool value)
        {
            canvasGroup.alpha = (value) ? 1 : 0;
            canvasGroup.interactable = value;
            canvasGroup.blocksRaycasts = value;
        }
    }
}