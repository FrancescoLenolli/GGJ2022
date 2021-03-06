using System.Collections;
using UnityEngine;

public class ChoiceSystem : MonoBehaviour
{
    [SerializeField] private Choice startingChoice = null;
    [Tooltip("Choice selected when the player has no more good tokens.")]
    [SerializeField] private Choice goodEnding = null;
    [Tooltip("Choice selected when the player has no more bad tokens.")]
    [SerializeField] private Choice badEnding = null;
    [Tooltip("How many good and bad tokens does the player have at the start of the game?")]
    [SerializeField] private int startingTokenCount = 3;

    private int goodTokenCount;
    private int badTokenCount;
    // How many choices are made during a game.
    private int choiceCounter;
    private float singleTokenValue;
    private Choice currentChoice;

    private void Start()
    {
        goodTokenCount = startingTokenCount;
        badTokenCount = startingTokenCount;
        singleTokenValue = 1f / (startingTokenCount * 2);
        currentChoice = startingChoice;

        EventManager.StartListening("UpdateTokens", UpdateTokensCount);
        EventManager.StartListening("NextChoice", SelectNewChoice);
        EventManager.StartListening("RestartGame", RestartGame);
        StartCoroutine(DelayedStartRoutine());

    }

    private void SelectNewChoice(object value)
    {
        Choice newChoice = (Choice)value;

        if (goodTokenCount <= 0)
        {
            currentChoice = goodEnding;
            PlayChoice();
            return;
        }
        if (badTokenCount <= 0)
        {
            currentChoice = badEnding;
            PlayChoice();
            return;
        }

        currentChoice = newChoice;
        PlayChoice();
    }

    private void UpdateTokensCount(object value)
    {
        Choice.ChoiceType choiceType = (Choice.ChoiceType)value;
        float gradientChangeValue = 0f;

        switch (choiceType)
        {
            case Choice.ChoiceType.Good:
                goodTokenCount--;
                badTokenCount++;
                gradientChangeValue = +singleTokenValue;
                break;
            case Choice.ChoiceType.Bad:
                goodTokenCount++;
                badTokenCount--;
                gradientChangeValue = -singleTokenValue;
                break;
            case Choice.ChoiceType.Neutral:
                break;
        }

        EventManager.TriggerEvent("UpdateBackgroundGradient", gradientChangeValue);
    }

    private void PlayChoice()
    {
        EventManager.TriggerEvent("ChangeChoice", currentChoice);

        if (currentChoice == startingChoice)
        {
            ResetGame();
            return;
        }
        if (currentChoice == goodEnding /*|| currentChoice == badEnding*/)
        {
            //FadeReset();
            EventManager.TriggerEvent("TriggerEnding", Choice.ChoiceType.Good);
            return;
        }
        if(currentChoice == badEnding)
        {
            EventManager.TriggerEvent("TriggerEnding", Choice.ChoiceType.Bad);
            return;
        }
        if (currentChoice.finalChoice)
        {
            EventManager.TriggerEvent("TriggerEnding", Choice.ChoiceType.Neutral);
            return;
        }

        choiceCounter++;
    }

    private void ResetGame()
    {
        EventManager.TriggerEvent("ResetView", 0);
        goodTokenCount = startingTokenCount;
        badTokenCount = startingTokenCount;
        choiceCounter = 0;
    }

    private void FadeReset()
    {
        EventManager.TriggerEvent("Fade", 0);
    }

    private void RestartGame(object value)
    {
        currentChoice = startingChoice;
        ResetGame();
        EventManager.TriggerEvent("ChangeChoice", currentChoice);
    }

    private IEnumerator DelayedStartRoutine()
    {
        yield return new WaitForSeconds(.1f);
        PlayChoice();
        yield return null;
    }
}
