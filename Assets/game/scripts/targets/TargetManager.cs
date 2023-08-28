using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace Targets
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager instance { get; private set; }

        [Header("Resources")]

        public List<Target> registeredTargets = new List<Target>();
        public Material wireframeMaterial;

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

        public Target GetTargetByID(byte id)
        {
            if (registeredTargets == null || registeredTargets.Count <= 0)
            {
                Debug.LogError("Registered targets list is null or empty!");
                return null;
            }

            foreach(Target target in registeredTargets)
            {
                if (target.id == id)
                {
                    return target;
                }
            }

            Debug.LogError("Could not find any target with the id " + id);
            return null;
        }

        public void EnableWireframe()
        {
            if (TargetObject.instance == null)
            {
                return;
            }

            TargetObject.instance.meshRenderer.materials = new Material[2]
            {
                TargetObject.instance.meshRenderer.material,
                wireframeMaterial
            };
        }

        public void DisableWireframe()
        {
            if (TargetObject.instance == null)
            {
                return;
            }

            TargetObject.instance.meshRenderer.materials = new Material[1]
            {
                TargetObject.instance.meshRenderer.material,
            };
        }
    }
}