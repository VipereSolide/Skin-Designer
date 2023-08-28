using System.Collections.Generic;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine;

namespace Frames
{
    public class Frame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public static Frame instance { get; private set; }

        public bool hover;

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            hover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hover = false;
        }
    }
}