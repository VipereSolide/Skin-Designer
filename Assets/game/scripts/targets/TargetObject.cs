using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using Inspector;
using Texture;

namespace Targets
{
    public class TargetObject : MonoBehaviour
    {
        public static TargetObject instance { get; private set; }

        public Target target;

        [Header("Resources")]

        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;
        public Texture2D defaultEmissionTexture;

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
            Init(target);
        }

        public void Init(Target target)
        {
            this.target = target;

            Material usedMaterial = new Material(target.defaultMaterial);
            usedMaterial = SetupMaterialKeywords(usedMaterial);
            meshRenderer.material = usedMaterial;
        }

        public static Material SetupMaterialKeywords(Material mat)
        {
            string[] keywords = new string[]
            {
                "_ALPHABLEND_ON",
                "_ALPHAPREMULTIPLY_ON",
                "_ALPHATEST_ON",
                "_DETAIL_MULX2",
                "_EMISSION",
                "_METALLICGLOSSMAP",
                "_NORMALMAP",
                "_OCCLUSION",
                "_PARALLAXMAP",
                "_SPECCGLOSSMAP",
                "_SPECGLOSSMAP"
            };

            foreach (string keyword in keywords)
            {
                mat.EnableKeyword(keyword);
            }

            return mat;
        }

        public static string TextureIndexToTextureName(int i)
        {
            switch(i)
            {
                case 0: return "_MainTex";
                case 1: return "_MetallicGlossMap";
                case 2: return "_BumpMap";
                case 3: return "_EmissionMap";
                case 4: return "_DetailAlbedoMap";
                case 5: return "_ParallaxMap";
                case 6: return "_OcclusionMap";
                default:
                    Debug.LogError("Cannot get texture name for texture index " + i);
                    return "_MainTex";
            }
        }

        public void SetMaterialTexture(int i, Texture2D texture)
        {
            meshRenderer.material.SetTexture(TextureIndexToTextureName(i), texture);

            if (i == 3)
            {
                Color color = (texture == null || texture == defaultEmissionTexture) ? Color.black : Color.white;
                meshRenderer.material.SetColor("_EmissionColor", color);
            }
        }
    }
}