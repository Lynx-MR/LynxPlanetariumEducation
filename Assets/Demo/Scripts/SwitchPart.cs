using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lynx.PlanetariumEdu
{ 
    public class SwitchPart : MonoBehaviour


    {
        [SerializeField] public List<GameObject> Enabled;
        [SerializeField] public List<GameObject> Disabled;


        public void GoPartUp()
        {
            foreach (GameObject go in Enabled) 
            {
                go.SetActive(true);
            }
            foreach (GameObject go in Disabled)
            {
                go.SetActive(false);
            }
        }



        public void GoPartDown()
        {
            foreach (GameObject go in Enabled)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in Disabled)
            {
                go.SetActive(true);
            }
        }

    }
}