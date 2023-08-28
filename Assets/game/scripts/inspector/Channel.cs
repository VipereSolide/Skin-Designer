using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using Texture;

namespace Inspector
{
    [System.Serializable]
    public class Channel
    {
        public string channelName;
        public bool enabled;

        [Space]
        public TextureHolder texture;
    }
}