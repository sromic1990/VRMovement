using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sourav.VRMovement
{
    public class PlayerTargetMovement : MonoBehaviour
    {
        public GameObject GroundPlane;
        public Transform MainCam;

        private Ray ray;
        private RaycastHit rayHit;

	    // Use this for initialization
	    void Awake ()
        {
		    
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
            Debug.DrawRay(MainCam.position, MainCam.forward * 75, Color.green);

            if(Input.GetMouseButtonUp(0))
            {
                PointClickRayCast();
            }
	    }

        private void PointClickRayCast()
        {
            ray = new Ray(MainCam.position, MainCam.forward);

            if(Physics.Raycast(ray, out rayHit, Mathf.Infinity))
            {
                GameObject obj = rayHit.collider.gameObject;
                if(obj == GroundPlane)
                {
                    transform.position = rayHit.point;
                }
            }
        }
    }
}
