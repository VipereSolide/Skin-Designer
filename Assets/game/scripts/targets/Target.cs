using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace Targets
{
    [CreateAssetMenu(menuName = "Target")]
    public class Target : ScriptableObject
    {
        public byte id;
        public string title;

        [Space]

        public GameObject prefab;
        public Material defaultMaterial;
    }
}