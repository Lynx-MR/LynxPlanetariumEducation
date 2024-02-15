using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Lynx.PlanetariumEdu
{ 
    public class QCM_Answer : MonoBehaviour
    {
        protected XRGrabInteractable _intObj;
        [SerializeField] private QCM_Manager manager;
        [SerializeField] private int answerIndex;

        private int linkedAnswerBox = 9;

        protected virtual void Start()
        {
            _intObj = GetComponent<XRGrabInteractable>();
            _intObj.firstSelectEntered.AddListener(onGraspBegin);
            _intObj.lastSelectExited.AddListener(onGraspEnd);
        }

        private void onGraspBegin(SelectEnterEventArgs arg)
        {
            manager.UncheckBox(answerIndex);
            manager.DisableGraspHint();
            Debug.Log("Grasp begin");
        }


        private void onGraspEnd(SelectExitEventArgs arg)
        {
            manager.checkAnswer(answerIndex);
            manager.endButttonActivationCheck();
            Debug.Log("grasp end");
        }


        public void linkToBox(int b)
        {
            linkedAnswerBox = b;
        }

        public int GetLinkedBox()
        {
            return linkedAnswerBox;
        }
    }
}
