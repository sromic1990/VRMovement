using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.VRMovement
{
    public class FirstPersonTeleport : MonoBehaviour
    {
        public Transform CameraTransform;
        public Image BlinkImage;
        public float BlinkSpeed = 0.3f;

        private Ray ray;
        private RaycastHit raycastHit;
        private GameObject currentWaypoint;
        private LayerMask wayPointLayer;



	    void Awake ()
        {
            wayPointLayer = LayerMask.GetMask("WayPoint");
            BlinkImage.canvasRenderer.SetAlpha(0);
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
            WaypointRaycast();
	    }

        private void WaypointRaycast()
        {
            ray = new Ray(CameraTransform.position, CameraTransform.forward);

            if(Physics.Raycast(ray, out raycastHit, 200f, wayPointLayer))
            {
                GameObject hitObject = raycastHit.collider.gameObject;

                if(hitObject != currentWaypoint)
                {
                    if(currentWaypoint != null)
                    {
                        ResetPreviousWaypoint();
                    }

                    TargetNewWaypoint(hitObject);
                }

                if(Input.GetMouseButtonDown(0))
                {
                    BlinkImage.CrossFadeAlpha(1f, BlinkSpeed, false);
                }

                else if(Input.GetMouseButtonUp(0))
                {
                    transform.position = hitObject.transform.position;
                    transform.rotation = hitObject.transform.rotation;
                    BlinkImage.CrossFadeAlpha(0f, BlinkSpeed, false);
                }
            }
            else if(currentWaypoint != null)
            {
                ResetPreviousWaypoint();
            }
        }

        private void TargetNewWaypoint(GameObject wayPoint)
        {
            wayPoint.GetComponent<WaypointBehavior>().OrbTargeted();
            currentWaypoint = wayPoint;
        }

        private void ResetPreviousWaypoint()
        {
            currentWaypoint.GetComponent<WaypointBehavior>().OrbReset();
            currentWaypoint = null;
        }
    }
}

