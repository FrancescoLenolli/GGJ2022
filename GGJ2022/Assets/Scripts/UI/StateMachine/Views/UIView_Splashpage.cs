using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.StateMachine;
using System;

public class UIView_Splashpage : UIView
{
    [SerializeField] private float fadeTime = 1f;

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

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine(fadeTime));
    }

    private IEnumerator FadeOutRoutine(float fadeTime)
    {
        float time = fadeTime;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            canvasGroup.alpha = time / fadeTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
