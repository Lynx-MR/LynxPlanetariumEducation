using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{ 
    public class HandMenu_ButtonFix : MonoBehaviour
    {
        [SerializeField] private Collider[] buttonHitBox;


        public void DisableButtons(float time)
        {
            for(int i = 0; i<buttonHitBox.Length; i++)
            {
                buttonHitBox[i].enabled = false;
            }
            Invoke("EnableButtons", time);
        }

        private void EnableButtons()
        {
            for (int i = 0; i < buttonHitBox.Length; i++)
            {
                buttonHitBox[i].enabled = true;
            }
        }
    }
}