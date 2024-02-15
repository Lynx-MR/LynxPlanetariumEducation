using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.PlanetariumEdu
{ 
    public class SwitchMat : MonoBehaviour
    {
        public List<GameObject> BlueRing;
        public List<GameObject> RedRing;
        public List<GameObject> BluePanel;
        public List<GameObject> RedPanel;

        public Color RingRedColor = Color.red;
        public Color RingBlueColor = Color.blue;
        public Color PanelRedColor = Color.red;
        public Color PanelBlueColor = Color.blue;
        public Color ColorWHITE = Color.white;
        private Vector4 ColorBlack = new Vector4(0.1294f, 0.1294f, 0.1294f, 1.0f);

        public void Setcolor()
        {
            foreach (GameObject ring in BlueRing)
            {
                ring.GetComponent<Renderer>().material.color = RingBlueColor;
            }

            foreach (GameObject ring in RedRing)
            {
                ring.GetComponent<Renderer>().material.color = RingRedColor;
            }

            foreach (GameObject panel in BluePanel)
            {
                panel.GetComponent<SpriteRenderer>().color = PanelBlueColor;
            }

            foreach (GameObject panel in RedPanel)
            {
                panel.GetComponent<SpriteRenderer>().color = PanelRedColor;
            }
        }
        public void ResetColor()
        {
            foreach (GameObject ring in BlueRing)
            {
                ring.GetComponent<Renderer>().material.color = ColorWHITE;
            }

            foreach (GameObject ring in RedRing)
            {
                ring.GetComponent<Renderer>().material.color = ColorWHITE;
            }

            foreach (GameObject panel in BluePanel)
            {
                panel.GetComponent<SpriteRenderer>().color = ColorBlack;
            }

            foreach (GameObject panel in RedPanel)
            {
                panel.GetComponent<SpriteRenderer>().color = ColorBlack;
            }
        }
        
    }
}