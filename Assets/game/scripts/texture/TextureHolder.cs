using System.Collections.Generic;
using System.Collections;
using System.IO;

using UnityEngine;
using System;

namespace Texture
{
    [System.Serializable]
    public class TextureHolder
    {
        public string path;
        public Texture2D loaded;
        
        public byte[] data { get; private set; }

        public bool isLoaded { get; private set; } = false;

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(path);
        }

        public Texture2D Load(bool destroyOnloadEmpty = false)
        {
            if (IsEmpty())
            {
                if (destroyOnloadEmpty)
                {
                    path = string.Empty;
                    isLoaded = false;
                    loaded = null;
                }
                else
                {
                    Debug.LogError("Cannot load texture for null or empty path!");
                }

                return loaded;
            }

            data = File.ReadAllBytes(path);
            GC.Collect();

            loaded = new Texture2D(2, 2);
            loaded.LoadImage(data);
            loaded.Apply();

            isLoaded = true;
            GC.Collect();
            return loaded;
        }

        public Texture2D LoadIfChanged(int comparedLength)
        {
            byte[] newData = File.ReadAllBytes(path);
            // Debug.Log("Comparing new texture data length " + newData.Length + " to old texture data length " + comparedLength);

            if (newData.Length != comparedLength)
            {
                // Debug.Log("Texture data length not matching, loading new data length");
                loaded = new Texture2D(2, 2);
                loaded.LoadImage(newData);
                loaded.Apply();

                return loaded;
            }

            return null;
        }

        public Texture2D GetOrLoad()
        {
            if (IsEmpty())
            {
                return null;
            }

            if (isLoaded)
            {
                return loaded;
            }

            return Load();
        }

        public TextureHolder(string path, bool load = false)
        {
            this.path = path;

            if (load)
            {
                Load();
            }
            else
            {
                loaded = null;
            }
        }

        public TextureHolder(string path, Texture2D texture)
        {
            this.path = path;
            loaded = texture;
        }

        public TextureHolder()
        {
            path = string.Empty;
            loaded = null;
        }
    }
}