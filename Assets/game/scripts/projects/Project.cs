using System.Collections.Generic;
using System.Collections;

using UnityEngine;

using Texture;

namespace Projects
{
    [System.Serializable]
    public class Project
    {
        [Header("Settings")]

        public string path;

        [Header("Textures")]

        public string[] texturePaths = new string[7] { "default", "default", "default", "default", "default", "default", "default" };

        public Project(string path, string a, string m, string n, string e, string d, string h, string o)
        {
            this.path = path;
            texturePaths[0] = a;
            texturePaths[1] = m;
            texturePaths[2] = n;
            texturePaths[3] = e;
            texturePaths[4] = d;
            texturePaths[5] = h;
            texturePaths[6] = o;
        }

        public Project(string a, string m, string n, string e, string d, string h, string o)
        {
            texturePaths[0] = a;
            texturePaths[1] = m;
            texturePaths[2] = n;
            texturePaths[3] = e;
            texturePaths[4] = d;
            texturePaths[5] = h;
            texturePaths[6] = o;
        }

        public Project(string a, string m, string n, string e, string d, string h)
        {
            texturePaths[0] = a;
            texturePaths[1] = m;
            texturePaths[2] = n;
            texturePaths[3] = e;
            texturePaths[4] = d;
            texturePaths[5] = h;
        }

        public Project(string a, string m, string n, string e, string d)
        {
            texturePaths[0] = a;
            texturePaths[1] = m;
            texturePaths[2] = n;
            texturePaths[3] = e;
            texturePaths[4] = d;
        }

        public Project(string a, string m, string n, string e)
        {
            texturePaths[0] = a;
            texturePaths[1] = m;
            texturePaths[2] = n;
            texturePaths[3] = e;
        }

        public Project(string a, string m, string n)
        {
            texturePaths[0] = a;
            texturePaths[1] = m;
            texturePaths[2] = n;
        }

        public Project(string a, string m)
        {
            texturePaths[0] = a;
            texturePaths[1] = m;
        }

        public Project(string a)
        {
            texturePaths[0] = a;
        }

        public Project()
        {

        }
    }
}