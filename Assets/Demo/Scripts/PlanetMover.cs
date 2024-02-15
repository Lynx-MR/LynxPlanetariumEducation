using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Lynx.PlanetariumEdu
{ 
    public class PlanetMover : MonoBehaviour
    {
        [Tooltip("The transform this object revolves around. Always the Sun, except for the moon.")]
        public Transform center;

        [Tooltip("Degrees per second this object moves around its orbit.")]
        public float speedRevolution = 10;


        [Tooltip("The axis of rotation around its poles, ie, the direction from the planet's south pole to the north pole. ")]
        public Vector3 axis = Vector3.up;


        [Tooltip("Degrees per second the object rotates on its own axis. ")]
        public float speed = 10.0f;

   
        private Vector3 dir;

    
        private bool Aligning = false;
        private bool isAligned = false;
        private float alignementSpeed = 100F;
        private float rotationFactor = 1F;
        private float alignementFator = 1;

        private float lastDist = 1000;
        private void Start()
        {
            dir = center.up; //Get the axis of rotation from the object we're rotating. 

        }


        // Update is called once per frame
        void Update () 
        {
            if(!Aligning)
            {
                transform.RotateAround(center.position, center.TransformDirection(dir), Time.deltaTime * speedRevolution * rotationFactor); //Rotating around the sun (orbit).
                transform.Rotate(axis, speed * Time.deltaTime * rotationFactor, Space.Self); //Rotating around its own axis (night/day).
                lastDist = 1000;

            }
            else if(Aligning && !isAligned)
            {
                Vector3 targetPosition = new Vector3(Vector3.Distance(center.position, transform.position) + center.position.x, center.position.y, center.position.z);
                if (Vector3.Distance(transform.position, targetPosition) > 0.01 && Vector3.Distance(transform.position, targetPosition) <= lastDist) // if planets ar not aligned
                {
                    lastDist = Vector3.Distance(transform.position, targetPosition);
                    float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
                    transform.RotateAround(center.position, center.TransformDirection(dir), Time.deltaTime * distanceToTarget * (alignementSpeed * alignementFator));
                }
                else
                    isAligned = true;
                transform.Rotate(axis, speed * Time.deltaTime, Space.Self); //Rotating around its own axis (night/day).
            }
        }
        public void AlignPlanets()
        {
            if(transform.position.z < center.position.z)
            {
                alignementFator = -1;
            }
            else
            {
                alignementFator = 1;
            }
            Aligning = true;
            isAligned = false;
            lastDist = 1000;
        }
        public void RotatePlanet()
        {
            Aligning = false;
        }
        public void ChangeRevolutionSpeed(float x)
        {
            rotationFactor = x;
        }

    }
}