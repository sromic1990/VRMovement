using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sourav.VRMovement
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonMovement : MonoBehaviour
    {
        public float MovementSpeed = 1.5f;
        public bool ComfortMode = false;
        public float ComfortRotationAngle = 45f;

        private CharacterController characterController;
        private Transform MainCameraTransform;
        private bool walking = false;
        private bool rotating = false;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            MainCameraTransform = transform.GetChild(0).transform;
        }
	
	    // Update is called once per frame
	    void Update ()
        {
            if (Input.GetMouseButtonUp(0))
                walking = !walking;

            if(ComfortMode && ComfortRotationAngle > 0 && !walking)
            {
                EnableCameraSnap();
            }

            if(walking)
            {
                MoveForward();
            }
	    }

        private void MoveForward()
        {
            Vector3 fixedForwardDirection = transform.forward * MovementSpeed;
            Vector3 cameraDirection = MainCameraTransform.forward * MovementSpeed;

            Vector3 moveDirection = ComfortMode ? fixedForwardDirection : cameraDirection;

            characterController.SimpleMove(moveDirection);
        }

        private void EnableCameraSnap()
        {
            var horizontalAxis = Input.GetAxis("Horizontal");
            if(horizontalAxis > 0)
            {
                SnapCameraRotation(ComfortRotationAngle);
            }
            else if (horizontalAxis < 0)
            {
                SnapCameraRotation(-1 * ComfortRotationAngle);
            }
            else
            {
                rotating = false;
            }
        }

        private void SnapCameraRotation(float degrees)
        {
            if(!rotating)
            {
                transform.Rotate(0, degrees, 0);
                rotating = true;
            }
        }
    }
}

