using System.Collections.Generic;
using System.Collections;
using System.IO;

using UnityEngine.EventSystems;
using UnityEngine;

using TMPro;
using SFB;

namespace Inspector
{
    public class ExplorerPathPicker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Resources")]
        
        public GameObject hoverObject;
        public TMP_InputField texturePathField;

        private bool hover;
        private string storedLastLocation;

        private void Start()
        {
            storedLastLocation = PlayerPrefs.GetString("storedLastLocationTexture");
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetString("storedLastLocationTexture", storedLastLocation);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            string[] paths = StandaloneFileBrowser.OpenFilePanel("Open Project", storedLastLocation, "png", false);

            if (paths == null || paths.Length == 0)
            {
                Debug.LogWarning("No texture were selected to be opened.");
                return;
            }

            storedLastLocation = Path.GetDirectoryName(paths[0]);
            texturePathField.text = paths[0];
            ChannelViewer.instance.UpdateState();
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
        }
    }
}