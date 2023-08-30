using System.Collections.Generic;
using System.Collections;

using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace Core
{
    public class GraphicsManager : MonoBehaviour
    {
        public static GraphicsManager instance { get; private set; }

        [Header("Resources")]

        public PostProcessVolume volume;

        private void RegisterSingleton()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            Destroy(this);
        }

        private void Awake()
        {
            RegisterSingleton();
        }

        public AmbientOcclusion GetAmbientOcclusion()
        {
            return volume.profile.GetSetting<AmbientOcclusion>();
        }

        public Bloom GetBloom()
        {
            return volume.profile.GetSetting<Bloom>();
        }

        public void SetPostProcessingActive(bool active)
        {
            volume.gameObject.SetActive(active);
        }

        public void SetAmbientOcclusion(bool active)
        {
            GetAmbientOcclusion().active = active;
        }

        public void SetBloom(bool active)
        {
            GetBloom().active = active;
        }

        public bool IsPostProcessingActive()
        {
            return volume.gameObject.activeSelf;
        }

        public bool IsAmbientOcclusionActive()
        {
            return GetAmbientOcclusion().active;
        }

        public bool IsBloomActive()
        {
            return GetBloom().active;
        }
    }
}