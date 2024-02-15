using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{ 
    public class PlanetariumManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES
        public GameObject planetariumParent;

        public PlanetMover[] Planets = null;

        public GameObject Cam;
        public GameObject Earth;
        public float EarthDistance = 1.0F;
        public float EarthZoomedSize = 2.0F;
        public Renderer sunRenderer;
        public Material sunARMat;
        public Material sunVRMat;
        public Renderer SkyboxRenderer;

        public InfoPanel_Manager panels;

        #endregion

        #region SCRIPT VARIABLES
        private IEnumerator zoomCoroutine;
        private Vector3 planetariumInitPos;
        private Vector3 earthBaseSize;

        #endregion

        #region PUBLIC FUNCTION

        public void SetAR(bool isAR)
        {
            if (isAR)
            {
                Vector3 valueSet = Cam.transform.forward;
                print(valueSet);
                SkyboxRenderer.material.SetVector("_VeiwAxis", new Vector4(valueSet.x, valueSet.y, valueSet.z, 0F));
                sunRenderer.material = sunARMat;
                StartCoroutine(SkyboxEffect(0, 1, 4));
            }
            else
            {
                Vector3 valueSet = Cam.transform.forward * -1;
                print(valueSet);
                SkyboxRenderer.material.SetVector("_VeiwAxis", new Vector4(valueSet.x, valueSet.y, valueSet.z, 0F));
                sunRenderer.material = sunVRMat;
                StartCoroutine(SkyboxEffect(1, 0, 4));
            }
        }

        public void AlignPlanets()
        {
            for(int i =0; i<Planets.Length; i++)
            {
                Planets[i].AlignPlanets();
            }
        }
        public void RotatePlanets()
        {
            for (int i = 0; i < Planets.Length; i++)
            {
                Planets[i].RotatePlanet();
            }
        }

        public void ZoomOnEarth()
        {
            //calculate offset 
            Vector3 earthTarget = Cam.transform.position + Cam.transform.forward * EarthDistance;
            Vector3 earthMovement = earthTarget - Earth.transform.position;
            zoomCoroutine = Zoom(planetariumParent, planetariumParent.transform.position, planetariumParent.transform.position + earthMovement);
            StartCoroutine(zoomCoroutine);
            StartCoroutine(SmoothScale(Earth, Earth.transform.localPosition, Earth.transform.localPosition, earthBaseSize.x, EarthZoomedSize));

        }
        public void ResetZoom()
        {
            //calculate offset 
            Vector3 planetariumMovement = planetariumInitPos - planetariumParent.transform.position;
            zoomCoroutine = Zoom(planetariumParent, planetariumParent.transform.position, planetariumParent.transform.position + planetariumMovement);
            StartCoroutine(zoomCoroutine);
            StartCoroutine(SmoothScale(Earth, Earth.transform.localPosition, Earth.transform.localPosition, EarthZoomedSize, earthBaseSize.x));

        }
        public void setRotationSpeed(float x)
        {
            for (int i = 0; i < Planets.Length; i++)
            {
                Planets[i].ChangeRevolutionSpeed(x);
            }
        }

        public void ScalePlanetarium(float targetSize)
        {
            float actualSize = planetariumParent.transform.localScale.x;
            Vector3 zoomPivot = new Vector3(Vector3.Distance(planetariumParent.transform.position, Earth.transform.position) + planetariumParent.transform.position.x, planetariumParent.transform.position.y, planetariumParent.transform.position.z);
            StartCoroutine(SmoothScale(planetariumParent, planetariumParent.transform.position, zoomPivot, actualSize, targetSize));
        }


        #endregion

        #region PRIVATE FUNCTION
        private void Start()
        {
            planetariumInitPos = planetariumParent.transform.position;
            earthBaseSize = Earth.transform.localScale;
        }
        private float EaseInOutCubic(float x)
        {
            if (x < 0.5) return 4F * x * x * x;
            else return 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        }

        #endregion

        #region COROUTINE
        IEnumerator Zoom(GameObject parent, Vector3 basePos,Vector3 target)
        {
            for(float t = 0F; t<1; t+= Time.deltaTime)
            {
                if(t>1) { t = 1; }
                parent.transform.position = Vector3.Lerp(basePos, target, EaseInOutCubic(t));
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator SmoothScale(GameObject parent,Vector3 parentPos, Vector3 pivot, float baseScale, float targetScale)
        {
            //calculate parent position offset to keep pivot stil
            Vector3 A = parentPos;
            Vector3 B = pivot;
            Vector3 C = A - B; //diff postion from parent/pivot
            float RS = targetScale / baseScale; // scale factor
            Vector3 PO = B + C * RS; //position offset
            print("A = " + A +"    //    B = " + pivot + "    //    RS = " + RS);

            for (float t = 0F; t < 1; t += Time.deltaTime)
            {
                if (t > 1) { t = 1; }
                parent.transform.localScale = Vector3.one * Mathf.Lerp(baseScale, targetScale, EaseInOutCubic(t));
                parent.transform.localPosition = Vector3.Lerp(A, PO, EaseInOutCubic(t));
                yield return new WaitForEndOfFrame();
            }
        }


        IEnumerator SkyboxEffect(float startValue, float endValue, float time)
        {
            for (float t = 0F; t < 1; t += Time.deltaTime/time)
            {
                float valueSet = Mathf.Lerp(startValue, endValue, t);
                SkyboxRenderer.material.SetFloat("_Switch", valueSet);

                yield return new WaitForEndOfFrame();
            }
        }

        #endregion

    }
}