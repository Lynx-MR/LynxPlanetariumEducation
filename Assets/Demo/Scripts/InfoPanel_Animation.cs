using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{ 
    public class InfoPanel_Animation : MonoBehaviour
    {

        #region PUBLIC FUNCTION
        public void OpenPanel()
        {
            StartCoroutine(AnimPanel(this.gameObject, 0F, 1F));
        }
        public void ClosePanel()
        {
            StartCoroutine(AnimPanel(this.gameObject, 1F, 0F));
        }
        #endregion


        #region PRIVATE FUNCTION

        private float EaseInOutCubic(float x)
        {
            if (x < 0.5) return 4F * x * x * x;
            else return 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        }

        #endregion

        #region COROUTINE
        IEnumerator AnimPanel(GameObject parent, float baseScale, float targetScale)
        {
            for (float t = 0F; t < 1; t += Time.deltaTime)
            {
                if (t > 1) { t = 1; }
                parent.transform.localScale = Vector3.one * Mathf.Lerp(baseScale, targetScale, EaseInOutCubic(t));
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion
    }
}