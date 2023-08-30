using System.Collections.Generic;
using System.Collections;

using UnityEngine;

using UI;
using Core;

namespace SettingsMenu
{
    public class PostProcessingOption : MonoBehaviour
    {
        public enum OptionType
        {
            Global,
            AmbientOcclusion,
            Bloom
        }

        public OptionType optionType;

        [Header("Resources")]

        public CustomToggle toggle;

        private void Start()
        {
            UpdateUI();

            toggle.onChangeState.AddListener(OnChangeState);
        }

        private void UpdateUI()
        {
            bool active = false;

            switch (optionType)
            {
                case OptionType.Global:
                    active = GraphicsManager.instance.IsPostProcessingActive();
                    break;
                case OptionType.AmbientOcclusion:
                    active = GraphicsManager.instance.IsAmbientOcclusionActive();
                    break;
                case OptionType.Bloom:
                    active = GraphicsManager.instance.IsBloomActive();
                    break;
            }

            toggle.activated = active;
        }

        private void OnChangeState()
        {
            bool active = toggle.activated;

            switch (optionType)
            {
                case OptionType.Global:
                    GraphicsManager.instance.SetPostProcessingActive(active);
                    break;
                case OptionType.AmbientOcclusion:
                    GraphicsManager.instance.SetAmbientOcclusion(active);
                    break;
                case OptionType.Bloom:
                    GraphicsManager.instance.SetBloom(active);
                    break;
            }
        }
    }
}