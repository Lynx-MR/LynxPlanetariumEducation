using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{ 
    public class LookAtCamera : MonoBehaviour
    {

        void OnWillRenderObject()
        {
            FaceCamera(Camera.current);
        }

        void FaceCamera(Camera cam)
        {
            Camera targetcam = cam; 
            if (transform.position - targetcam.transform.position == Vector3.zero)
            {
                return;
            }

            transform.rotation = Quaternion.LookRotation(transform.position - targetcam.transform.position);
        }
    }
}