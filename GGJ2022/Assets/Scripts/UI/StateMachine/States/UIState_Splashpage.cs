using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.StateMachine;

public class UIState_Splashpage : UIState
{
    private UIView_Splashpage view;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = owner.Root.splashpageView;
        view.OnPlay += StartGame;
        view.OnQuit += QuitGame;
    }

    public override void ShowState()
    {
        base.ShowState();
        view.ShowView();
    }

    public override void HideState()
    {
        base.HideState();
        view.HideView();
    }

    private void StartGame()
    {
        owner.ChangeState(typeof(UIState_Main));
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
