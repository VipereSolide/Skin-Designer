using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace Cameras
{
    public class GlobalCameraManager : MonoBehaviour
    {
        public static GlobalCameraManager instance { get; private set; }

        [Header("Resources")]

        public Camera targetCamera;

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

        public void SetTargetCameraOrtographic()
        {
            targetCamera.orthographic = true;
        }

        public void SetTargetCameraPerspective()
        {
            targetCamera.orthographic = false;
        }
    }
}