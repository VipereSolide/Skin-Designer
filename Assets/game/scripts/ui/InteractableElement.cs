using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [ExecuteAlways]
    public class InteractableElement : MonoBehaviour
    {
        [Header("Toggle")]

        public bool linkToggle;
        public CustomToggle toggle;
        public bool inverse;

        [Header("State")]

        [SerializeField]
        private bool isInteractable = true;

        private CanvasGroup group;

        public bool interactable { get { return isInteractable; } set { isInteractable = value; UpdateGraphics(); } }

        [UnityEngine.ContextMenu("Init")]
        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            if (linkToggle)
            {
                UpdateToggleState();
                toggle.onChangeState.AddListener(() => { UpdateToggleState(); });
            }
        }

        private void UpdateToggleState()
        {
            interactable = (inverse) ? !toggle.activated : toggle.activated;
        }

        private void OnValidate()
        {
            UpdateGraphics();
        }

        private void UpdateGraphics()
        {
            if (group == null)
            {
                return;
            }

            group.alpha = (isInteractable) ? 0 : 1;
            group.interactable = !isInteractable;
            group.blocksRaycasts = !isInteractable;
        }
    }
}