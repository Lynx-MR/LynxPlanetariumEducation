using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{ 
    public class UI_Animation : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform = null;

        void Start()
        {
            //Fetch the RectTransform from this GameObject
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }
        }

        #region PUBLIC FUNCTION

        public void ScaleTo(float targetSize, float time)
        {
            float actualSize = rectTransform.transform.localScale.x;
            StartCoroutine(ScalePanel(actualSize, targetSize, time));
        }

        public void SetScale(float targetedSize)
        {
            rectTransform.transform.localScale = Vector3.one * targetedSize;
        }

        public void MoveTo(Vector3 targetPos, float time)
        {
            Vector3 actPos = rectTransform.anchoredPosition3D;
            StartCoroutine(ScalePanel(actPos, targetPos, time));
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
        IEnumerator ScalePanel(float baseScale, float targetScale, float timeTo)
        {
            for (float t = 0F; t < 1; t += Time.deltaTime/timeTo)
            {
                if (t > 1) { t = 1; }
                rectTransform.transform.localScale = Vector3.one * Mathf.Lerp(baseScale, targetScale, EaseInOutCubic(t));
                yield return new WaitForEndOfFrame();
            }
        }
        IEnumerator ScalePanel(Vector3 basePos, Vector3 targetPos, float timeTo)
        {
            for (float t = 0F; t < 1; t += Time.deltaTime / timeTo)
            {
                if (t > 1) { t = 1; }
                rectTransform.anchoredPosition3D = Vector3.Lerp(basePos, targetPos, EaseInOutCubic(t));
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion
    }
}