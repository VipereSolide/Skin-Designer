using System.Collections.Generic;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

using ContextMenus;
using TMPro;
using Core;

namespace Header
{
    public class HeaderOptionItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Settings")]

        public string title;

        [Header("State")]

        public bool hover;

        [Header("Resources")]

        public ContextMenus.ContextMenu menu;
        
        [Space]

        public GameObject hoverGameObject;
        public TMP_Text titleText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            hover = true;

            UpdateGraphics();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (menu.isEnabled)
            {
                return;
            }

            hover = false;

            UpdateGraphics();
        }

        private void UpdateGraphics()
        {
            hoverGameObject.SetActive(hover);
            titleText.text = title;
        }

        private void Start()
        {
            UpdateGraphics();

            menu.onClose.AddListener(() =>
            {
                hover = false;

                UpdateGraphics();
            });
        }

        private void OnValidate()
        {
            UpdateGraphics();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (menu == null)
            {
                return;
            }

            menu.Open();
        }
    }
}