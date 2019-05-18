using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PetProject
{
    public class FloatIdle : MonoBehaviour
    {
        private enum Direction
        {
            UP = 1,
            DOWN = -1,
        };

        [Header("Settings")]
        public float BobSpeed;
        public float TopMovementClamp;
        public float BottomMovementClamp;

        private Transform _mTransform;
        private float _movementAmount;
        private Direction _direction = Direction.UP;
        // Start is called before the first frame update
        void Start()
        {
            _mTransform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            SetDirection();
            Move();
        }

        private void Move()
        {
            float floatMovement = BobSpeed * Time.deltaTime * (int)_direction;
            Vector3 movement = new Vector3(0.0f, floatMovement, 0.0f);
            _mTransform.Translate(movement, Space.Self);
            _movementAmount += floatMovement;
        }

        private void SetDirection()
        {
            if(_movementAmount >= TopMovementClamp)
            {
                _direction = Direction.DOWN;
            }
            else if (_movementAmount <= BottomMovementClamp)
            {
                _direction = Direction.UP;
            }
        }
    }

}
