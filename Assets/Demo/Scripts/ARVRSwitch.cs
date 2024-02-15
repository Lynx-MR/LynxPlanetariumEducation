using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu 
{ 
    public class ARVRSwitch : MonoBehaviour
    {
        [SerializeField] private Renderer SkyboxRenderer;
        [SerializeField] private Renderer sunRenderer;
        [SerializeField] private Material sunARMat;
        [SerializeField] private Material sunVRMat;
        public void SetAR(bool isAR)
        {
            if (isAR)
            {
                sunRenderer.material = sunARMat;
                StartCoroutine(SkyboxEffect(0, 1, 4));
            }
            else
            {
                sunRenderer.material = sunVRMat;
                StartCoroutine(SkyboxEffect(1, 0, 4));
            }
        }


        IEnumerator SkyboxEffect(float startValue, float endValue, float time)
        {
            for (float t = 0F; t < 1; t += Time.deltaTime / time)
            {
                float valueSet = Mathf.Lerp(startValue, endValue, t);
                SkyboxRenderer.material.SetFloat("_Switch", valueSet);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}