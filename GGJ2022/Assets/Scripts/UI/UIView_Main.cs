using UIFramework.StateMachine;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIView_Main : UIView
{
    [SerializeField] private TextMeshProUGUI labelQuery = null;
    [SerializeField] private GameObject buttonChoicePrefab = null;
    [SerializeField] private Transform buttonsContainer = null;
    [SerializeField] private GameObject collectiblePrefab = null;
    [SerializeField] private Transform collectiblesContainer = null;
    [SerializeField] private BackgroundGradient backgroundGradient = null;

    public void SetUp(object value)
    {
        foreach(Transform child in buttonsContainer)
        {
            Destroy(child.gameObject);
        }
        Choice newChoice = (Choice)value;

        labelQuery.text = newChoice.choiceQuery;
        if(newChoice.collectibleSprite)
        {
            Image collectibleImage = Instantiate(collectiblePrefab, collectiblesContainer).GetComponent<Image>();
            collectibleImage.sprite = newChoice.collectibleSprite;
        }

        for(int i = 0; i < newChoice.sections.Count; ++i)
        {
            int index = i;
            GameObject newButton = Instantiate(buttonChoicePrefab, buttonsContainer);
            Button button = newButton.GetComponent<Button>();
            TextMeshProUGUI buttonLabel = newButton.GetComponentInChildren<TextMeshProUGUI>();

            UnityAction unityAction = null;
            unityAction = () =>
            {
                EventManager.TriggerEvent("UpdateTokens", newChoice.sections[index].status);
                EventManager.TriggerEvent("NextChoice", newChoice.sections[index].nextChoice);
            };

            button.onClick.AddListener(unityAction);
            buttonLabel.text = newChoice.sections[i].text;
        }
    }

    public void UpdateBackgroundGradient(object value)
    {
        float gradientChangeValue = (float)value;
        backgroundGradient.ChangeGradient(gradientChangeValue);
    }

    public void ResetView(object value)
    {
        backgroundGradient.ResetGradient();
        foreach(Transform child in collectiblesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
