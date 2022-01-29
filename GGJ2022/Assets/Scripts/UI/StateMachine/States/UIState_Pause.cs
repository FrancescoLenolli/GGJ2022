using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.StateMachine;

public class UIState_Pause : UIState
{
    private UIView_Pause view;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = owner.Root.pauseView;

        view.OnResume += ResumeGame;
        view.OnBackToMain += BackToMainMenu;
        view.OnRestart += RestartGame;
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

    private void ResumeGame()
    {
        owner.ChangeState(typeof(UIState_Main));
    }

    private void BackToMainMenu()
    {
        owner.ChangeState(typeof(UIState_Splashpage));
        EventManager.TriggerEvent("RestartGame", null);
    }

    private void RestartGame()
    {
        owner.ChangeState(typeof(UIState_Main));
        EventManager.TriggerEvent("RestartGame", null);
    }
}
