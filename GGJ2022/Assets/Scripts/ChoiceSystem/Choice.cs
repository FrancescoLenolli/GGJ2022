using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Choice", fileName = "NewChoice")]
public class Choice : ScriptableObject
{
    public enum ChoiceType { Good, Bad }

    [System.Serializable]
    public struct DialogueSection
    {
        public ChoiceType status;
        [TextArea]
        public string text;
        public Choice nextChoice;
    }

    [Tooltip("Is this one of the ends?")]
    public bool finalChoice = false;
    public string choiceQuery = "Query";
    public List<DialogueSection> sections = new List<DialogueSection>();
}
