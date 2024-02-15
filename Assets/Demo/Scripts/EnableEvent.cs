using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{
    public class EnableEvent : MonoBehaviour
    {
        public GameObject handHint;
        private void Awake()
        {
            handHint.SetActive(false);
        }

    }
}