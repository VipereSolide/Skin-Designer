using System.Collections.Generic;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine;

using TMPro;
using Core;

namespace Inspector
{
    public class ChannelListItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public int channel;

        [Space]

        public bool selected;
        public bool hover;

        [Space]

        public string shortcutName;

        [Header("Resources")]

        public TMP_Text channelNameText;
        public GameObject selectedGameObject;
        public GameObject hoverGameObject;

        public void OnPointerDown(PointerEventData eventData)
        {
            Select();
        }

        public void Select()
        {
            if (this.selected)
            {
                return;
            }

            ChannelList.instance.RequestSelected(channel);
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

        public Channel GetChannel()
        {
            return ChannelManager.instance.channels[channel];
        }

        public void UpdateGraphics()
        {
            channelNameText.text = GetChannel().channelName[0].ToString().ToUpper();

            if (selected)
            {
                selectedGameObject.SetActive(true);

                if (hoverGameObject.activeSelf)
                {
                    hoverGameObject.SetActive(false);
                }
            }
            else
            {
                if (selectedGameObject.activeSelf)
                {
                    selectedGameObject.SetActive(false);
                }

                if (hover)
                {
                    hoverGameObject.SetActive(true);
                    return;
                }

                hoverGameObject.SetActive(false);
            }
        }

        public void SetSelected(bool selected)
        {
            this.selected = selected;
            UpdateGraphics();
        }

        private void Update()
        {
            if (ShortcutManager.GetShortcut(shortcutName).IsPressed())
            {
                Select();
            }
        }
    }
}