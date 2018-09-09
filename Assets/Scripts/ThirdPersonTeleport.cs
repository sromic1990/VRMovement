using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sourav.VRMovement
{
    public class ThirdPersonTeleport : MonoBehaviour
    {
        public Transform Player;
        public Transform CameraTransform;

        private Ray ray;
        private RaycastHit raycastHit;
        private LayerMask groundMask;

	    void Awake ()
        {
            groundMask = LayerMask.GetMask("Ground");
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
		    if(Input.GetMouseButtonUp(0))
            {
                PointClickRaycast();
            }
	    }

        private void PointClickRaycast()
        {
            ray = new Ray(CameraTransform.position, CameraTransform.forward);

            if(Physics.Raycast(ray, out raycastHit, Mathf.Infinity, groundMask))
            {
                NavMeshHit navMeshHit;
                if(NavMesh.SamplePosition(raycastHit.point, out navMeshHit, 0.52f, NavMesh.AllAreas))
                {
                    Player.position = navMeshHit.position;
                }
            }
        }
    }
}
