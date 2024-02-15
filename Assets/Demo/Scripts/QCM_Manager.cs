using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lynx.PlanetariumEdu
{ 
    public class QCM_Manager : MonoBehaviour
    {
        #region SCRIPT INPUT

        [SerializeField] private GameObject[] validationBoxs;
        [SerializeField] private QCM_Answer[] answers;
        [SerializeField] private float checkDistance;
        [SerializeField] private Image[] questionBG;

        [SerializeField] private GameObject endButton;

        [SerializeField] private Color goodAnswer;
        [SerializeField] private Color badAnswer;
        [SerializeField] private GameObject graspHint;
        [SerializeField] private GameObject tapHint;

        #endregion

        private bool[] answerState = new bool[10];
        private bool[] usedBox = new bool[10];

        void Awake()
        {
            for (int i = 0; i < validationBoxs.Length; i++)
            {
                answerState[i] = false;
                usedBox[i] = false;
            }
        }

        public void UncheckBox(int index)
        {
            usedBox[answers[index].GetLinkedBox()] = false;
            answerState[answers[index].GetLinkedBox()] = false;
            answers[index].linkToBox(9);
        }

        public void DisableGraspHint()
        {
            graspHint.SetActive(false);
        }
        public void EnableTapHint(bool state)
        {
            tapHint.SetActive(state);
        }
        public void checkAnswer(int index)
        {
            GameObject answerChecked = answers[index].gameObject;
            float minDist = 1000000;
            int closestBoxIndex = 9;
            GameObject closestBox = answerChecked;
            for(int i = 0; i < validationBoxs.Length; i++)
            {
                float dist = Vector3.Distance(answerChecked.transform.position, validationBoxs[i].transform.position);
                if(dist< minDist && !usedBox[i])
                {
                    minDist = dist;
                    closestBox = validationBoxs[i];
                    closestBoxIndex = i;
                }
            }


            if(minDist< checkDistance)
            {
                answerChecked.transform.position = closestBox.transform.position;
                answerState[closestBoxIndex] = (index == closestBoxIndex);
                answers[index].linkToBox(closestBoxIndex);
                usedBox[closestBoxIndex] = true;
            }
        }

        public void endButttonActivationCheck()
        {
            bool activationState = true;
            for(int i = 0; i< questionBG.Length; i++)
            {
                if (!usedBox[i])
                {
                    activationState = false;
                }
            }
            endButton.SetActive(activationState);
            tapHint.SetActive(activationState);
        }
    
        public void AnswerValidationCheck()
        {
            tapHint.SetActive(false);
            for(int i = 0; i< questionBG.Length; i++)
            {
                if(answerState[i] == false)
                {
                    questionBG[i].color = badAnswer;
                }
                else
                {
                    questionBG[i].color = goodAnswer;
                }
            }
        }


    }
}