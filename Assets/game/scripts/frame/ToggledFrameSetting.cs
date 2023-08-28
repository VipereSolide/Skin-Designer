using System.Collections.Generic;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace Frames
{
    public class ToggledFrameSetting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Sprites")]

        public Sprite stateOn;
        public Sprite stateOff;

        [Header("State")]

        public bool hover;
        public bool state;

        [Space]

        public UnityEvent onActivate;
        public UnityEvent onDisable;

        [Header("Resources")]

        public GameObject hoverObject;
        public Image image;

        private void Start()
        {
            UpdateGraphics();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            state = !state;

            if (state) onActivate?.Invoke();
            else onDisable?.Invoke();

            UpdateGraphics();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hover = true;

            UpdateGraphics();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hover = false;

            UpdateGraphics();
        }

        private void UpdateGraphics()
        {
            hoverObject.SetActive(hover);
            image.sprite = (state) ? stateOn : stateOff;
        }
    }
}