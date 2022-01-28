using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundGradient : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    private Image image;
    private float time = .5f;
    private float startingTime;

    private void Awake()
    {
        startingTime = time;
        image = GetComponent<Image>();
    }

    public void ChangeGradient(float timeIncrease)
    {
        time += timeIncrease;
        image.color = gradient.Evaluate(time);
    }

    public void ResetGradient()
    {
        time = startingTime;
        image.color = gradient.Evaluate(time);
    }
}
