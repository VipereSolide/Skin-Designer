using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [ExecuteAlways]
    public class InteractableElement : MonoBehaviour
    {
        [SerializeField]
        private bool isInteractable = true;

        private CanvasGroup group;

        public bool interactable { get { return isInteractable; } set { isInteractable = value; UpdateGraphics(); } }

        [UnityEngine.ContextMenu("Init")]
        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
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