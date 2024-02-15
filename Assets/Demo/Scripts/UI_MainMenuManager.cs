using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lynx.PlanetariumEdu
{ 
    public class UI_MainMenuManager : MonoBehaviour
    {
        public UI_Animation[] classButtons;
        public GameObject subClassParent;
        public UI_Animation[] subClassButton;
        public GameObject chapterParent;
        public UI_Animation[] chapterButton;
        public float buttonLockTime = 1;
        private int stage = 0; // 0 = Class slection / 1 = subclass selection / 2 = chapter selection
        private bool isLocked = false;

        #region PRIVATE FUNCTION

        private void EnableSubClass()
        {
            subClassParent.SetActive(true);
            for (int i = 0; i < subClassButton.Length; i++)
            {
                subClassButton[i].SetScale(0F);
            }
            for (int i = 0; i < subClassButton.Length; i++)
            {
                Invoke("ScaleOutSubClass", 0.33F);
            }
        }
        private void ScaleOutSubClass()
        {
            stage = 1;
            for (int i = 0; i < subClassButton.Length; i++)
            {
                subClassButton[i].ScaleTo(1F, 0.25F);
              
            }
        }
        private void EnableChapter()
        {
            chapterParent.SetActive(true);
            for (int i = 0; i < chapterButton.Length; i++)
            {
                chapterButton[i].SetScale(0F);
            }
            for (int i = 0; i < chapterButton.Length; i++)
            {
                Invoke("ScaleOutChapters", 0.33F);
            }
        }
        private void ScaleOutChapters()
        {
            stage = 2;
            for (int i = 0; i < chapterButton.Length; i++)
            {
                chapterButton[i].ScaleTo(1F, 0.25F);
            }
        }

        private void EnableFunctions()
        {
            isLocked = false;
        }
        #endregion


        #region PUBLIC FUNCTION
        public void ClassSelection(int classIndex)
        {
            if (stage != 0 && !isLocked) // go back to classSelection if not in it
            {
                subClassParent.SetActive(false);
                chapterParent.SetActive(false);
                for (int i = 0; i < classButtons.Length; i++)
                {
                    if (i == classIndex)
                    {
                        classButtons[i].MoveTo(new Vector3(-46 + i * 23, 0, 0), 0.33F);
                    }
                    else
                    {
                        classButtons[i].ScaleTo(1F, 0.33F);
                    }
                }
                stage = 0; //reset stage to class selection menu
            }
            else if (!isLocked)// select a class if in classSelection menu
            {
                for (int i = 0; i < classButtons.Length; i++) 
                {
                    if (i == classIndex)
                    {
                        classButtons[i].MoveTo(new Vector3(-46, 0, 0), 0.33F);
                    }
                    else
                    {
                        classButtons[i].ScaleTo(0F, 0.33F);
                    }
                }
                EnableSubClass();
            }
            isLocked = true;
            Invoke("EnableFunctions", buttonLockTime);
        }

        public void SubClassSelection(int classIndex)
        {
            if (stage != 1 && !isLocked) // go back to subclass selection menu if not in it 
            {
                chapterParent.SetActive(false);
                for (int i = 0; i < subClassButton.Length; i++)
                {
                    if (i == classIndex)
                    {
                        subClassButton[i].MoveTo(new Vector3(12.1F, 19 - i * 19, 0), 0.33F);
                    }
                    else
                    {
                        subClassButton[i].ScaleTo(1F, 0.33F);
                    }
                }
                stage = 1; // reset stage to subclass seletion
            }
            else if (!isLocked) // close menu if in subclass selection menu
            {
                for (int i = 0; i < subClassButton.Length; i++)
                {
                    if (i == classIndex)
                    {
                        subClassButton[i].MoveTo(new Vector3(12.1F, 19, 0), 0.33F);
                    }
                    else
                    {
                        subClassButton[i].ScaleTo(0F, 0.33F);
                    }
                }
                EnableChapter();
            }
            isLocked = true;
            Invoke("EnableFunctions", buttonLockTime);
        }
        #endregion

    }
}