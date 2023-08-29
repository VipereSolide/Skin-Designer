using System.Collections.Generic;
using System.Collections;

using UnityEngine;

using Frames;
using Core;

namespace Cameras
{
    public class TargetCameraController : MonoBehaviour
    {
        [Header("Pivot")]

        public string pivotShortcutName = "Camera Pivot";
        public Transform pivotTransform;
        public float pivotSensitivity = 3.5f;

        [Header("Pan")]

        public string panShortcutName = "Camera Pan";
        public Transform panTransform;
        public float panSensitivity = 3.5f;

        [Header("Zooming")]

        public float zoomSensitivity = 4f;
        public float zoomSensitivityOrthographic = 1;
        public Camera targetCamera;

        [Header("Resources")]

        public Frame frame;

        private bool isPivotting = false;
        private bool isPanning = false;

        private void GetInputs()
        {
            if (Input.GetKeyUp(ShortcutManager.GetShortcut(pivotShortcutName).combination[0])) isPivotting = false;
            if (Input.GetKeyDown(ShortcutManager.GetShortcut(pivotShortcutName).combination[0]) && frame.hover) isPivotting = true;

            if (Input.GetKeyUp(ShortcutManager.GetShortcut(panShortcutName).combination[0])) isPanning = false;
            if (Input.GetKeyDown(ShortcutManager.GetShortcut(panShortcutName).combination[0]) && frame.hover) isPanning = true;
        }

        private void Update()
        {
            GetInputs();

            if (isPivotting)
            {
                Pivot();
            }

            if (isPanning)
            {
                Pan();
            }

            ZoomInOut();
        }

        private void ZoomInOut()
        {
            float zoomSpeed = Input.GetAxisRaw("Mouse ScrollWheel");

            if (zoomSpeed == 0)
            {
                return;
            }

            if (targetCamera.orthographic)
            {
                targetCamera.orthographicSize = Mathf.Clamp(targetCamera.orthographicSize - zoomSpeed * zoomSensitivityOrthographic, 0.25f, 6);
            }
            else
            {
                targetCamera.fieldOfView = Mathf.Clamp(targetCamera.fieldOfView - zoomSpeed * zoomSensitivity, 5, 60);
            }
        }

        private void Pivot()
        {
            float x = Input.GetAxisRaw("Mouse X");
            float y = -Input.GetAxisRaw("Mouse Y");

            pivotTransform.localEulerAngles += new Vector3(y, x, 0) * pivotSensitivity;
        }

        private void Pan()
        {
            float x = Input.GetAxisRaw("Mouse X");
            float y = Input.GetAxisRaw("Mouse Y");

            panTransform.localPosition += new Vector3(x, y, 0) * -1 * panSensitivity * Time.deltaTime;
        }
    }
}