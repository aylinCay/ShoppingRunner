using System;
using System.Collections;
using HypeFire.Library.Controllers.Move;
using HypeFire.Library.Controllers.Rotate;
using HypeFire.Library.Controllers.Swerve;
using HypeFire.Templates.Runner.CharacterController.Abstract;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShoppingRush
{
    [RequireComponent(typeof(RigidbodyMove), typeof(RotateController))]
    public class Player : MonoBehaviour, ICharacterController,IDragAndDropEvent
    {
        public float rotationOffset = 2;
        public float speed = 5f;
        public float jumpForce;

        [field: SerializeField] public bool isMoveAble { get; private set; }

        [field: SerializeField] public bool isJumpAble { get; set; }

        [field: SerializeField] public bool isRotateAble { get; private set; }

        [field: SerializeField] public bool isStopped { get; private set; }

        [field: SerializeField] public SwerveInputReader swerveReader { get; private set; }

        [field: SerializeField] public RigidbodyMove rigidbodyMove { get; private set; }

        [field: SerializeField] public RotateController rotateController { get; private set; }
        
        [field: SerializeField] public bool isImpact { get; set; }
        
        
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

                if (isMoveAble)
                {
                    Move(speed, transform.TransformDirection(Vector3.forward));
                }

                if (isJumpAble)
                {
                    Jump(jumpForce,Vector3.up);
                    isJumpAble = false;
                }
                
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
            rigidbodyMove.Jump(force,direction);
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

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Trampoline"))
            {
                isJumpAble = true;
            }

            if (collision.gameObject.CompareTag("Barrier"))
            {
                isImpact = true;
              Destroy(collision.gameObject);
            }
            
        }
    }
}