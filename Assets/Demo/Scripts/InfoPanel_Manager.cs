using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{ 
    public class InfoPanel_Manager : MonoBehaviour
    {
        [SerializeField] private GameObject[] planetsPanel;
        [SerializeField] private float scaleTime = 0.33F;

        private bool[] panelState;
        private bool isOpening = false;
        private void Start()
        {
            panelState = new bool[planetsPanel.Length];
            for (int i = 0; i < panelState.Length; i++)
            {
                panelState[i] = false;
            }
        }



        #region PUBLIC FUNCTION
        public void OpenInfo(int index)
        {
            if (isOpening) { return; }
            isOpening = true;
            for(int i = 0; i < planetsPanel.Length; i++)
            {
                if (i == index && !panelState[i])
                {
                    OpenPanel(i);
                }
                else
                {
                    ClosePanel(i);
                }
            }
            Invoke("Unlock", 1F);
        }

        #endregion

        #region COROUTINE
        IEnumerator ScalePanel(float baseScale, float targetScale, float timeTo, int i)
        {
            for (float t = 0F; t < 1; t += Time.deltaTime / timeTo)
            {
                if (t > 1) { t = 1; }
                planetsPanel[i].transform.localScale = Vector3.one * Mathf.Lerp(baseScale, targetScale, EaseInOutCubic(t));
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion

        #region PRIVATE FUNCTION
        private void Unlock()
        {
            isOpening = false;
        }
        private void OpenPanel(int i)
        {
            float currentScale = planetsPanel[i].transform.localScale.x;
            StartCoroutine(ScalePanel(currentScale, 1.0F, scaleTime,i));
            panelState[i] = true;
        }

        private void ClosePanel(int i)
        {
            float currentScale = planetsPanel[i].transform.localScale.x;
            StartCoroutine(ScalePanel(currentScale, 0.0F, scaleTime,i));
            panelState[i] = false;
        }


        private float EaseInOutCubic(float x)
        {
            if (x < 0.5) return 4F * x * x * x;
            else return 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        }

        #endregion

    }
}