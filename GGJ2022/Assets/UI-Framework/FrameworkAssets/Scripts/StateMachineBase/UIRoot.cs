using UnityEngine;

namespace UIFramework.StateMachine
{
    /// <summary>
    /// UI Root class, used for storing references to UI views and other useful references.
    /// </summary>
    public class UIRoot : MonoBehaviour
    {
        //[SerializeField]
        //private UIView uiView;

        //public UIView UiView => uiView;

        public UIView_Main mainView = null;
        public UIView_Splashpage splashpageView = null;
        public UIView_Pause pauseView = null;
    }
}
