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

        EventManager.StartListening("ChangeChoice", view.SetUp);
        EventManager.StartListening("UpdateBackgroundGradient", view.UpdateBackgroundGradient);
        EventManager.StartListening("ResetBackground", view.ResetBackgroundGradient);
    }
}
