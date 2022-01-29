using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundGradient : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    private Image image;
    private float gradientValue = .5f;
    private float targetValue;
    private float startingValue;

    private void Awake()
    {
        startingValue = gradientValue;
        targetValue = gradientValue;
        image = GetComponent<Image>();
    }

    public void ChangeGradient(float timeIncrease, float time)
    {
        targetValue += timeIncrease;
        StartCoroutine(ChangeGradientRoutine(time));
    }

    public void ResetGradient()
    {
        targetValue = startingValue;
        gradientValue = startingValue;
        image.color = gradient.Evaluate(gradientValue);
    }

    private IEnumerator ChangeGradientRoutine(float totalTime)
    {
        float time = 0f;
        while (time <= totalTime)
        {
            time += Time.deltaTime;
            float newValue = Mathf.Lerp(gradientValue, targetValue, time / totalTime);
            image.color = gradient.Evaluate(newValue);
            yield return null;
        }

        gradientValue = targetValue;
        image.color = gradient.Evaluate(gradientValue);
        yield return null;
    }
}
