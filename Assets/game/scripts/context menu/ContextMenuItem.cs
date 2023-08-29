using System.Collections.Generic;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

using TMPro;
using Core;

namespace ContextMenus
{
    public class ContextMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Settings")]

        public string title;

        [Space]
        public bool hasShortcut = false;
        public string shortcutName;

        [Space]
        public UnityEvent action;
        public bool closeOnClick;

        [Space]
        public UnityEvent onHover;
        public float onHoverTimer;

        [Header("State")]

        public bool hover;

        [Header("Resources")]

        public ContextMenu menu;
        
        [Space]

        public GameObject hoverGameObject;
        public TMP_Text text;
        public TMP_Text shortcutText;

        private void OnEnable()
        {
            hover = false;
            UpdateGraphics();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Execute();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hover = true;

            if (onHover != null)
            {
                Invoke(nameof(ExecuteHoverAction), onHoverTimer);
            }

            UpdateGraphics();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hover = false;

            UpdateGraphics();
        }

        public void Execute()
        {
            action?.Invoke();

            if (closeOnClick)
            {
                menu.Close();
            }
        }

        private void ExecuteHoverAction()
        {
            onHover?.Invoke();
        }

        private void UpdateGraphics()
        {
            hoverGameObject.SetActive(hover);

            text.text = title;

            if (hasShortcut)
            {
                shortcutText.text = ShortcutManager.GetShortcut(shortcutName).ToString();
            }
        }

        private void Start()
        {
            UpdateGraphics();
        }

        private void OnValidate()
        {
            UpdateGraphics();
        }

        private void Update()
        {
            if (hasShortcut == false)
            {
                return;
            }

            if (ShortcutManager.GetShortcut(shortcutName).IsPressed())
            {
                Execute();
            }
        }

        [UnityEngine.ContextMenu("Fetch Context Menu In Parents")]
        private void FetchContextMenuInParents()
        {
            FetchContextMenuInParentsReclusive(transform, 0);
        }

        private void FetchContextMenuInParentsReclusive(Transform transform, int tries)
        {
            if (tries > 20)
            {
                return;
            }

            menu = transform.GetComponent<ContextMenu>();
            if (menu == null && transform.parent != null)
            {
                FetchContextMenuInParentsReclusive(transform.parent, tries + 1);
            }
        }
    }
}