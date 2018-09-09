using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sourav.VRMovement
{
    public class RailMovement : MonoBehaviour
    {
        public float MovementSpeed = 1.5f;
        public float RotationSpeed = 10f;
        public Transform RailPath;

        private bool walking = false;
        private Transform targetWayPoint;
        private int wayPointCounter;

	    // Use this for initialization
	    void Awake ()
        {
            wayPointCounter = 0;
            targetWayPoint = RailPath.GetChild(wayPointCounter);
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
		    if(Input.GetMouseButtonUp(0))
            {
                walking = !walking;
            }

            if(walking)
            {
                EnableRailMovement();
            }
	    }

        private void EnableRailMovement()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, MovementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(NextTargetDirection());
            if(Vector3.Distance(targetWayPoint.position, transform.position) < 0.1f)
            {
                wayPointCounter++;
                try
                {
                    targetWayPoint = RailPath.GetChild(wayPointCounter);
                }
                catch
                {
                    Debug.Log("End of the path");
                }
            }
        }

        Vector3 NextTargetDirection()
        {
            float speed = MovementSpeed * Time.deltaTime;
            Vector3 rotationDirection = targetWayPoint.position - transform.position;

            return Vector3.RotateTowards(transform.forward, rotationDirection, speed, 0);
        }
    }
}
