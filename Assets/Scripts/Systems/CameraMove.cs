using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public class CameraMove : MonoBehaviour
    {
        [Header("Input")]
        public string horizontalInputName = "Horizontal";
        public string verticalInputName = "Vertical";
        public string rotateInputName = "Rotate";
        public string zoomInputName = "Zoom";
        public string resetInputName = "Reset";
        [HideInInspector] public float deltaTime;

        [Header("Settings")]
        public float panSpeed = 5.0f;
        public float rotationSpeed = 5.0f;
        public float zoomSpeed = 5.0f;

        [Header("Prototyping")]
        [SerializeField]
        private Transform _cursor; // TODO: Some kind of event to listen to for when the cursor enters an active state
        [SerializeField]
        private Transform _lockedOnTransform; // TODO: Use this

        private Vector3 _heading = Vector3.zero;
        private Vector3 _velocity = Vector3.zero;
        private float _movementAmount = 0.0f;
        private Transform _mTransform;
        private Vector3 _mTransformDefault;
        private Quaternion _mRotationDefault;
        private float _rotationAmount = 0.0f;
        private float _zoomAmount = 0.0f;
        private bool _reset = false;

        public CameraMove()
        {
            GameEventLibrary.CursorLockedOnEvent.RegisterListener(OnCursorLockedOn);
            GameEventLibrary.CursorLostLockOnEvent.RegisterListener(OnCursorLeftLockOn);
        }

        private void OnDestroy()
        {
            GameEventLibrary.CursorLockedOnEvent.UnregisterListener(OnCursorLockedOn);
            GameEventLibrary.CursorLostLockOnEvent.UnregisterListener(OnCursorLeftLockOn);
        }

        private void Start()
        {
            _cursor = FindObjectOfType<CursorStateController>()?.transform;
            _mTransform =  transform;
            _mTransformDefault = _mTransform.position;
            _mRotationDefault = transform.rotation;
        }

        private void Update()
        {
            deltaTime = Time.deltaTime;
            ReadInput();
            MoveCamera();
        }

        private void FixedUpdate()
        {
            SetVelocity();
        }

        private void SetVelocity()
        {
            //https://www.reddit.com/r/Unity3D/comments/7yi111/make_wasd_movement_based_on_camera_rotation/
            _velocity = Quaternion.AngleAxis(_mTransform.rotation.eulerAngles.y, Vector3.up) * _heading;
        }

        private void ReadInput()
        {
            if (Input.GetButtonDown(resetInputName))
            {
                _reset = true;
            }
            _heading = Vector3.zero;
            _heading.x = Input.GetAxisRaw(horizontalInputName);
            _heading.z = Input.GetAxisRaw(verticalInputName);
            // Clamp the magnitude to prevent diagonal speed increase 
            // but still allow <1f for controller sensitivity
            _heading = Vector3.ClampMagnitude(_heading, 1.0f);
            _zoomAmount = Input.GetAxisRaw(zoomInputName);
            _rotationAmount = Input.GetAxisRaw(rotateInputName);
            _movementAmount = Mathf.Clamp01(Mathf.Abs(_heading.x) + Mathf.Abs(_heading.z));
        }

        private void MoveCamera()
        {
            if (_reset)
            {
                ResetCamera();
                return;
            }
            // Pan
            transform.Translate(_velocity * deltaTime * panSpeed, Space.World);
            // Zoom
            transform.Translate(new Vector3(0.0f, 0.0f, _zoomAmount * deltaTime * zoomSpeed), Space.Self);
            // Rotate
            RotateCamera();
        }

        private void RotateCamera()
        {
            Vector3 rotation = new Vector3(0.0f, _rotationAmount * deltaTime * rotationSpeed, 0.0f);
            if (_cursor != null)
            {
                transform.RotateAround(_cursor.position, Vector3.up, _rotationAmount * rotationSpeed * deltaTime);
            }
            else
            {
                transform.Rotate(rotation, Space.World);
            }
        }

        private void ResetCamera()
        {
            _reset = false;
            _mTransform.position = _mTransformDefault;
            _mTransform.rotation = _mRotationDefault;
            _movementAmount = 0.0f;
            _heading = Vector3.zero;
            _velocity = Vector3.zero;
            _zoomAmount = 0.0f;
        }

        private void OnCursorLockedOn(GameObjectEventArgs browserHighlight)
        {
            _lockedOnTransform = browserHighlight.GameObject.transform;
        }

        private void OnCursorLeftLockOn(EmptyEventArgs args)
        {
            _lockedOnTransform = null;
        }
    }
}
