using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer = null;
    [SerializeField] private GameObject videoRenderer = null;
    [SerializeField] private VideoClip goodEnding = null;
    [SerializeField] private VideoClip badEnding = null;
    [SerializeField] private VideoClip trueEnding = null;

    private void Awake()
    {
        EventManager.StartListening("TriggerEnding", TriggerCutscene);
    }

    private void TriggerCutscene(object value)
    {
        videoPlayer.clip = null;
        Choice.ChoiceType choiceType = (Choice.ChoiceType)value;
        VideoClip videoClip = trueEnding;
        switch(choiceType)
        {
            case Choice.ChoiceType.Good:
                videoClip = goodEnding;
                break;
            case Choice.ChoiceType.Bad:
                videoClip = badEnding;
                break;
            case Choice.ChoiceType.Neutral:
                videoClip = trueEnding;
                break;
        }

        videoPlayer.clip = videoClip;
        videoRenderer.SetActive(true);
        StartCoroutine(CutsceneRoutine());
    }

    private IEnumerator CutsceneRoutine()
    {
        float waitTime = (float)(videoPlayer.clip.length + 1f);
        videoPlayer.Play();
        yield return new WaitForSeconds(waitTime);
        EventManager.TriggerEvent("CutsceneEnded", null);
        videoRenderer.SetActive(false);

        yield return null;
    }
}
