using UnityEngine;
using System.Collections;

namespace PetProject
{
    public class Billboard : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void BillboardTransform()
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
                _camera.transform.rotation * Vector3.up);
        }

        //Orient the camera after all movement is completed this frame to avoid jittering
        void LateUpdate()
        {
            BillboardTransform();
        }
    }
}