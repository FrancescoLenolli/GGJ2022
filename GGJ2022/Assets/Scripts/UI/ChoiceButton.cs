using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private Sprite goodChoiceSprite = null;
    [SerializeField] private Sprite badChoiceSprite = null;
    [SerializeField] private Sprite neutralChoiceSprite = null;
    [SerializeField] private Button button = null;

    public void SetState(Choice.ChoiceType choiceType)
    {
        SpriteState spriteState = button.spriteState;
        switch (choiceType)
        {
            case Choice.ChoiceType.Good:
                spriteState.pressedSprite = goodChoiceSprite;
                break;
            case Choice.ChoiceType.Bad:
                spriteState.pressedSprite = badChoiceSprite;
                break;
            case Choice.ChoiceType.Neutral:
                spriteState.pressedSprite = neutralChoiceSprite;
                break;
        }
        button.spriteState = spriteState;
    }
}
