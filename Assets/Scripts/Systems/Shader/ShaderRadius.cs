using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public class ShaderRadius : MonoBehaviour
    {
        [Header("Settings")]
        public string RadiusVarName = "_Radius";
        public float Radius = 1.0f;

        void Update()
        {
            Shader.SetGlobalFloat(RadiusVarName, Radius);
        }
    }
}
