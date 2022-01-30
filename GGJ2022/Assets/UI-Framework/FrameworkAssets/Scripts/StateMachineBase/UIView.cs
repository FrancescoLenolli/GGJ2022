using UnityEngine;

namespace UIFramework.StateMachine
{
    /// <summary>
    /// Holds logic for the UIView visual elements.
    /// </summary>
    public class UIView : MonoBehaviour
    {
        protected CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();    
        }

        /// <summary>
        /// Method called to show view.
        /// </summary>
        public virtual void ShowView()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// Method called to hide view.
        /// </summary>
        public virtual void HideView()
        {

            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
