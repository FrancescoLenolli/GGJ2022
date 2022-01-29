using UIFramework.StateMachine;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIView_Main : UIView
{
    [SerializeField] private TextMeshProUGUI labelQuery = null;
    [SerializeField] private GameObject buttonChoicePrefab = null;
    [SerializeField] private Transform buttonsContainer = null;
    [SerializeField] private GameObject collectiblePrefab = null;
    [SerializeField] private Transform collectiblesContainer = null;
    [SerializeField] private BackgroundGradient backgroundGradient = null;
    [SerializeField] private CanvasGroup endingBackground = null;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float endingFadeTime = 2f;

    private CanvasGroup labelQueryGroup;
    private Action onPause;

    public Action OnPause { get => onPause; set => onPause = value; }

    public void Init()
    {
        labelQueryGroup = labelQuery.GetComponent<CanvasGroup>();
    }

    public void PauseGame()
    {
        onPause?.Invoke();
    }

    public void SetUp(object value)
    {
        ResetChoice();
        Choice newChoice = (Choice)value;
        CreateChoice(newChoice);
    }

    public void UpdateBackgroundGradient(object value)
    {
        float gradientChangeValue = (float)value;
        backgroundGradient.ChangeGradient(gradientChangeValue, fadeTime);
    }

    public void ResetView(object value)
    {
        backgroundGradient.ResetGradient();
        foreach(Transform child in collectiblesContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void Fade(object value)
    {
        StartCoroutine(EndingFadeRoutine(endingFadeTime));
    }

    private void ResetChoice()
    {
        labelQueryGroup.alpha = 0;
        foreach (Transform child in buttonsContainer)
        {
            Destroy(child.gameObject);
        }
        buttonsContainer.DetachChildren();
    }

    private void CheckCollectible(Sprite collectibleSprite)
    {
        if (collectibleSprite)
        {
            Image collectibleImage = Instantiate(collectiblePrefab, collectiblesContainer).GetComponent<Image>();
            collectibleImage.sprite = collectibleSprite;
        }
    }

    private void CreateChoice(Choice newChoice)
    {
        labelQuery.text = newChoice.choiceQuery;
        CheckCollectible(newChoice.collectibleSprite);

        for (int i = 0; i < newChoice.sections.Count; ++i)
        {
            int index = i;
            GameObject newButton = Instantiate(buttonChoicePrefab, buttonsContainer);
            ChoiceButton choiceButton = newButton.GetComponent<ChoiceButton>();
            Button button = newButton.GetComponentInChildren<Button>();
            TextMeshProUGUI buttonLabel = newButton.GetComponentInChildren<TextMeshProUGUI>();

            UnityAction unityAction = null;
            unityAction = () =>
            {
                EventManager.TriggerEvent("UpdateTokens", newChoice.sections[index].status);
                EventManager.TriggerEvent("NextChoice", newChoice.sections[index].nextChoice);
            };

            button.onClick.AddListener(unityAction);
            buttonLabel.text = newChoice.sections[i].text;
            choiceButton.SetState(newChoice.sections[index].status);
        }

        StartCoroutine(ShowNewViewRoutine(fadeTime));
    }

    private IEnumerator ShowNewViewRoutine(float fadeTime)
    {
        List<CanvasGroup> buttonsCanvasGroup = new List<CanvasGroup>();
        foreach(Transform child in buttonsContainer)
        {
            buttonsCanvasGroup.Add(child.GetComponent<CanvasGroup>());
        }

        float time = 0f;
        while (time <= fadeTime)
        {
            time += Time.deltaTime;
            float alphaValue = time / fadeTime;

            foreach(CanvasGroup canvasGroup in buttonsCanvasGroup)
            {
                canvasGroup.alpha = alphaValue;
            }
            labelQueryGroup.alpha = alphaValue;
            yield return null;
        }

        labelQueryGroup.alpha = 1;
        foreach (CanvasGroup canvasGroup in buttonsCanvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }
        yield return null;
    }

    private IEnumerator EndingFadeRoutine(float fadeTime)
    {
        endingBackground.blocksRaycasts = true;

        float time = 0f;
        float halfFadeTime = fadeTime / 2;
        while (time <= halfFadeTime)
        {
            time += Time.deltaTime;
            endingBackground.alpha = time / halfFadeTime;
            yield return null;
        }

        endingBackground.alpha = 1f;
        time = halfFadeTime;
        EventManager.TriggerEvent("RestartGame", null);

        while (time >= 0)
        {
            time -= Time.deltaTime;
            endingBackground.alpha = time / halfFadeTime;
            yield return null;
        }

        endingBackground.alpha = 0;
        endingBackground.blocksRaycasts = false;
    }
}
