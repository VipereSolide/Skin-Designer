using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using TMPro;

namespace Inspector
{
    public class ChannelList : MonoBehaviour
    {
        public static ChannelList instance { get; private set; }
        
        public int tempDefaultSelectedChannel = 0;

        [Header("Resources")]

        public List<ChannelListItem> channels = new List<ChannelListItem>();
        public ChannelViewer channelViewer;

        private void Awake()
        {
            RegisterSingleton();
        }

        private void Start()
        {
            RequestSelected(tempDefaultSelectedChannel);
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

        public void RequestSelected(int index)
        {
            if (index > channels.Count - 1)
            {
                Debug.LogError("Requested channel index higher than channels bound size!");
                return;
            }

            if (channels[index].selected)
            {
                Debug.LogWarning("Cannot select already selected channel.", gameObject);
                return;
            }

            for (int i = 0; i < channels.Count; i++)
            {
                if (channels[i].selected)
                {
                    channels[i].SetSelected(false);
                    continue;
                }

                if (i == index)
                {
                    channels[i].SetSelected(true);
                    channelViewer.SetInspectedChannel(channels[i].channel);
                }
            }
        }

        public void RequestSelected(ChannelListItem channel)
        {
            if (channels.Contains(channel) == false)
            {
                Debug.LogError("Cannot select channel not included in channel list!", gameObject);
                return;
            }

            if (channel.selected)
            {
                Debug.LogWarning("Cannot select already selected channel.", gameObject);
                return;
            }

            for (int i = 0; i < channels.Count; i++)
            {
                if (channels[i].selected)
                {
                    channels[i].SetSelected(false);
                    continue;
                }

                if (channels[i] == channel)
                {
                    channels[i].SetSelected(true);
                    channelViewer.SetInspectedChannel(channels[i].channel);
                }
            }

        }
    }
}