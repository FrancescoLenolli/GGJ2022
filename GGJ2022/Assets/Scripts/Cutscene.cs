using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer = null;
    [SerializeField] private GameObject videoRenderer = null;

    private void Awake()
    {
        EventManager.StartListening("FinalChoice", TriggerCutscene);
    }

    private void TriggerCutscene(object value)
    {
        videoRenderer.SetActive(true);
        StartCoroutine(CutsceneRoutine());
    }

    private IEnumerator CutsceneRoutine()
    {
        float waitTime = (float)(videoPlayer.clip.length + 1);
        videoPlayer.Play();
        yield return new WaitForSeconds(waitTime);
        EventManager.TriggerEvent("CutsceneEnded", null);
        videoRenderer.SetActive(false);

        yield return null;
    }
}
