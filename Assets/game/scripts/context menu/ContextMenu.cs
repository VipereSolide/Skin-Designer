using System.Collections.Generic;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

using Utility;

namespace ContextMenus
{
    public class ContextMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Settings")]

        public bool disablesOnUnfocus = true;

        [Header("State")]

        public bool isEnabled = false;
        public bool hover = false;

        [Space]

        public UnityEvent onClose;
        public UnityEvent onOpen;

        [Header("Resources")]

        public CanvasGroup contentObject;
        public ContextMenuItem unofficialParent;

        private bool enableMouseListener = true;

        private void UpdateGraphics()
        {
            contentObject.SetActive(isEnabled);
        }

        private void Update()
        {
            if (disablesOnUnfocus == false || isEnabled == false || enableMouseListener == false)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (hover || (unofficialParent != null && unofficialParent.hover))
                {
                    return;
                }

                Close();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hover = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hover = true;
        }

        public void Close()
        {
            isEnabled = false;
            UpdateGraphics();

            onClose.Invoke();
        }

        private void EnableMouseListener()
        {
            enableMouseListener = true;
        }

        public void Open()
        {
            enableMouseListener = false;
            Invoke(nameof(EnableMouseListener), 0.1f);

            isEnabled = true;
            UpdateGraphics();

            onOpen.Invoke();
        }
    }
}