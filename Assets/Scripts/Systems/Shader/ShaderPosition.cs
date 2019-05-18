using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public class ShaderPosition : MonoBehaviour
    {
        [Header("Settings")]
        public string PositionVarName = "_Position";

        private Transform _mTransform;

        void Start()
        {
            _mTransform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            Shader.SetGlobalVector(PositionVarName, transform.position);
        }
    }
}
