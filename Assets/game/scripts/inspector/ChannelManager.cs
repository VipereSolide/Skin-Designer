using System.Collections.Generic;
using System.Collections;

using UnityEngine;

using Targets;

namespace Inspector
{
    public class ChannelManager : MonoBehaviour
    {
        public static ChannelManager instance
        {
            get;
            private set;
        }

        public List<Channel> channels = new List<Channel>();

        private void Awake()
        {
            RegisterSingleton();
        }

        private void RegisterSingleton()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            Destroy(this);
        }

        public void UpdateTargetTextureFromChannelTextures()
        {
            if (TargetObject.instance != null)
            {
                for (int i = 0; i < channels.Count; i++)
                {
                    TargetObject.instance.SetMaterialTexture(i, channels[i].texture.GetOrLoad());
                }
            }
        }
        
        public void UpdateTargetTextureFromChannel(int channel)
        {
            if (TargetObject.instance != null)
            {
                TargetObject.instance.SetMaterialTexture(channel, channels[channel].texture.GetOrLoad());
            }
        }

        public void SetTargetTextureFromChannel(int channel, Texture2D texture)
        {
            TargetObject.instance.SetMaterialTexture(channel, texture);
        }
    }
}