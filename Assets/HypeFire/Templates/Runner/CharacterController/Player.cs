using System;
using HypeFire.Library.Controllers.Move;
using HypeFire.Library.Controllers.Rotate;
using HypeFire.Library.Controllers.Swerve;
using HypeFire.Templates.Runner.CharacterController.Abstract;
using UnityEngine;

namespace HypeFire.Templates.Runner.CharacterController
{
    [RequireComponent(typeof(RigidbodyMove), typeof(RotateController))]
    public class Player : MonoBehaviour, ICharacterController
    {
        public float rotationOffset = 2;
        public float speed = 5f;

        [field: SerializeField] public bool isMoveAble { get; private set; }

        [field: SerializeField] public bool isJumpAble { get; set; }

        [field: SerializeField] public bool isRotateAble { get; private set; }

        [field: SerializeField] public bool isStopped { get; private set; }

        [field: SerializeField] public SwerveInputReader swerveReader { get; private set; }

        [field: SerializeField] public RigidbodyMove rigidbodyMove { get; private set; }

        [field: SerializeField] public RotateController rotateController { get; private set; }
        
        private Vector3 _rotationPosition = Vector3.zero;

      

        public virtual void Start()
        {
            
            swerveReader = new SwerveInputReader(transform, Camera.main);
        }

        public virtual void Update()
        {
            if (isStopped) return;

            if (Input.GetButton("Fire1"))
            {
                isRotateAble = true;
                
            }
            if (Input.GetButtonDown("Fire1"))
            {
                isMoveAble = true;
                rigidbodyMove.isAutoMoveEnabled = true;
            }
            
        }

        public virtual void FixedUpdate()
        {
            if (isStopped) return;

            if (isRotateAble)
            {
                _rotationPosition.z = transform.position.z + rotationOffset;
                _rotationPosition.x = swerveReader.GetHorizontalPosition();
                _rotationPosition.y = swerveReader.GetVerticalPosition();
                 Rotate(_rotationPosition);
                
                if (_rotationPosition.y >.5f)
                {
                    isJumpAble = true;
                }
            }
            if (isMoveAble)
            {
                Move(speed, transform.TransformDirection(Vector3.forward));
            }

            if (isJumpAble)
            {
                Jump(7f,Vector3.up);
            }
        }

        public virtual  void Move(float speed, Vector3 direction)
        {
            rigidbodyMove.Move(speed, direction);
        }
        
        public virtual void Rotate(Vector3 direction)
        {
            rotateController.RotateToPosition(direction);
        }

        public virtual void Jump(float force, Vector3 direction)
        {
              transform.position += direction * force * Time.deltaTime;

              isJumpAble = false;
        }

        public virtual void Stop()
        {
            var rot = transform.position + Vector3.forward * 5f;
            Rotate(rot);
            isMoveAble = false;
            isRotateAble = false;
            isJumpAble = false;
            rigidbodyMove.isAutoMoveEnabled = false;
            isStopped = true;
        }
        
    }
}