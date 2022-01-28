using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Choice", fileName = "NewChoice")]
public class Choice : ScriptableObject
{
    public enum ChoiceType { Neutral, Good, Bad }

    [System.Serializable]
    public struct DialogueSection
    {
        public ChoiceType status;
        [TextArea]
        public string text;
        public Choice nextChoice;
    }

    [Tooltip("Check if this is the final choice.")]
    public bool finalChoice = false;
    public string choiceQuery = "Query";
    [Tooltip("Leave empty if this Choice doesn't have a collectible.")]
    public Sprite collectibleSprite = null;
    public List<DialogueSection> sections = new List<DialogueSection>();
}
