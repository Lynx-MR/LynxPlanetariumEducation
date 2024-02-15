using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{ 
    public class LockInFront : MonoBehaviour
    {
        [SerializeField] private float followStrengh = 2;
        [SerializeField] private float Offset;
        [SerializeField] private float StartThreshold;
        [SerializeField] private float StopThreshold;
        private GameObject cam;
        private bool isFollowing = false;

        private void Awake()
        {
             cam = Camera.main.gameObject;
        }
        // Update is called once per frame
        void Update()
        {
            Transform target = cam.transform;
            Vector3 flattenY = new Vector3(1, 0, 1);
            Vector3 newTransform = cam.transform.position + Vector3.Scale(cam.transform.forward, flattenY).normalized* Offset;
            if(Vector3.Distance(newTransform, transform.position) > StartThreshold && !isFollowing)
            {
                isFollowing = true;
                Debug.Log("StartFollowing");
                StartCoroutine(GoInFront());
            }

            //transform.position = Vector3.Lerp(this.transform.position, newTransform, followStrengh * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(this.transform.rotation, target.rotation, followStrengh * Time.deltaTime); 
        }

        IEnumerator GoInFront()
        {
            Transform target = cam.transform;
            Vector3 flattenY = new Vector3(1, 0, 1);
            Vector3 newTransform = cam.transform.position + Vector3.Scale(cam.transform.forward, flattenY).normalized * Offset;
            while(Vector3.Distance(newTransform,transform.position)>StopThreshold)
            {
                newTransform = cam.transform.position + Vector3.Scale(cam.transform.forward, flattenY).normalized * Offset;
                transform.position = Vector3.Lerp(this.transform.position, newTransform, followStrengh * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
                yield return new WaitForEndOfFrame();
            }
            isFollowing = false;
        }

    }
}