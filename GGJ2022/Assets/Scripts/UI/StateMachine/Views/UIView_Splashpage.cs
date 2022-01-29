using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.StateMachine;
using System;

public class UIView_Splashpage : UIView
{
    private Action onPlay;
    private Action onQuit;

    public Action OnPlay { get => onPlay; set => onPlay = value; }
    public Action OnQuit { get => onQuit; set => onQuit = value; }

    public void PlayGame()
    {
        onPlay?.Invoke();
    }

    public void QuitGame()
    {
        onQuit?.Invoke();
    }
}
