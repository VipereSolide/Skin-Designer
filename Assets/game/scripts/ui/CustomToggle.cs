using System.Collections.Generic;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

namespace UI
{
    public class CustomToggle : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private bool isActivated;
        public UnityEvent onChangeState;

        [Header("Resources")]

        public GameObject graphic;

        public bool activated
        {
            get { return isActivated; }
            set
            {
                isActivated = value;
                UpdateGraphics();
            }
        }

        public void Toggle()
        {
            activated = !activated;
            onChangeState?.Invoke();

            UpdateGraphics();
        }

        private void Start()
        {
            UpdateGraphics();
        }

        private void OnValidate()
        {
            UpdateGraphics();
        }

        public void UpdateGraphics()
        {
            if (graphic == null)
            {
                return;
            }

            graphic.SetActive(activated);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Toggle();
        }
    }
}