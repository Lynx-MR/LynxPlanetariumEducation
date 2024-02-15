using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{ 
    public class Group_StateManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Objects;


        public void DisableObjects()
        {
            for(int i = 0; i< Objects.Length; i++)
            {
                Objects[i].SetActive(false);
            }
        }

        public void EnableObjects()
        {

            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i].SetActive(true);
            }

        }
    }
}