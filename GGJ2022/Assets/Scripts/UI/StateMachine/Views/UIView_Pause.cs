using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.StateMachine;

public class UIView_Pause : UIView
{
    private Action onResume;
    private Action onBackToMain;
    private Action onRestart;

    public Action OnResume { get => onResume; set => onResume = value; }
    public Action OnBackToMain { get => onBackToMain; set => onBackToMain = value; }
    public Action OnRestart { get => onRestart; set => onRestart = value; }

    public void ResumeGame()
    {
        onResume?.Invoke();
    }

    public void BackToMain()
    {
        onBackToMain?.Invoke();
    }

    public void RestartGame()
    {
        onRestart?.Invoke();
    }
}
