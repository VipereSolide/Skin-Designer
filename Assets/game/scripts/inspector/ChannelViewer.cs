using System.Collections.Generic;
using System.Collections;
using System.IO;

using UnityEngine.UI;
using UnityEngine;

using Targets;
using TMPro;
using UI;

namespace Inspector
{
    public class ChannelViewer : MonoBehaviour
    {
        public static ChannelViewer instance { get; private set; }

        public int channel;

        [Header("Resources")]

        public GameObject unloadedTextureGameObject;
        public RawImage channelTextureHolder;

        [Space]

        public CustomToggle enabledToggle;
        public TMP_InputField texturePathInputField;

        [Space]

        public InteractableElement[] interactableElements;

        private int storedTextureDataLength;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            Destroy(this);
        }

        private void Start()
        {
            enabledToggle.onChangeState.AddListener(UpdateState);
            texturePathInputField.onSubmit.AddListener(e => UpdateState());
        }

        public Channel GetChannel()
        {
            return ChannelManager.instance.channels[channel];
        }

        public void UpdateState()
        {
            GetChannel().enabled = enabledToggle.activated;

            if (GetChannel().texture.path != texturePathInputField.text)
            {
                GetChannel().texture.path = texturePathInputField.text;
                GetChannel().texture.Load(true);
            }

            UpdateCurrentTexture();
            UpdateInteractables();
        }

        private void UpdateCurrentTexture()
        {
            if (GetChannel().texture.IsEmpty())
            {
                texturePathInputField.text = "";

                if (TargetObject.instance == null)
                {
                    unloadedTextureGameObject.SetActive(true);
                }
                else
                {
                    UnityEngine.Texture materialTexture = TargetObject.instance.target.defaultMaterial.GetTexture(TargetObject.TextureIndexToTextureName(channel));

                    unloadedTextureGameObject.SetActive(materialTexture == null);
                    ChannelManager.instance.channels[channel].enabled = (materialTexture != null);

                    channelTextureHolder.texture = materialTexture;
                    ChannelManager.instance.SetTargetTextureFromChannel(channel, (Texture2D) materialTexture);
                }
            }
            else
            {
                unloadedTextureGameObject.SetActive(false);

                channelTextureHolder.texture = GetChannel().texture.GetOrLoad();
                ChannelManager.instance.UpdateTargetTextureFromChannel(channel);

                texturePathInputField.text = GetChannel().texture.path;
            }
        }

        private void UpdateInteractables()
        {
            foreach (InteractableElement element in interactableElements)
            {
                element.interactable = enabledToggle.activated;
            }
        }

        public void InitChannel()
        {
            if (GetChannel().texture.IsEmpty())
            {
                texturePathInputField.text = "";
            }
            else
            {
                texturePathInputField.text = GetChannel().texture.path;
            }

            UpdateCurrentTexture();

            enabledToggle.activated = GetChannel().enabled;
            UpdateInteractables();
        }

        private void UpdateChannelTexture()
        {

        }

        private void OnApplicationFocus(bool focus)
        {
            if (GetChannel() == null || GetChannel().texture.IsEmpty() || GetChannel().texture.data == null)
            {
                return;
            }

            if (focus == false)
            {
                storedTextureDataLength = GetChannel().texture.data.Length;
                // Debug.Log("Stored current channel texture data length in memory: " + storedTextureDataLength);
            }
            else
            {
                // Debug.Log("Retrieved current channel texture data length from memory: " + storedTextureDataLength);
                GetChannel().texture.LoadIfChanged(storedTextureDataLength);
                UpdateCurrentTexture();
            }
        }

        public void SetInspectedChannel(int channel)
        {
            this.channel = channel;

            InitChannel();
        }
    }
}