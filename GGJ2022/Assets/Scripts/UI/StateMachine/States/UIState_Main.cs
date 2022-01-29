using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.StateMachine;

public class UIState_Main : UIState
{
    private UIView_Main view;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = owner.Root.mainView;
        view.Init();
        view.OnPause += PauseGame;

        EventManager.StartListening("ChangeChoice", view.SetUp);
        EventManager.StartListening("UpdateBackgroundGradient", view.UpdateBackgroundGradient);
        EventManager.StartListening("ResetView", view.ResetView);
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

    private void PauseGame()
    {
        owner.ChangeState(typeof(UIState_Pause));
    }
}
