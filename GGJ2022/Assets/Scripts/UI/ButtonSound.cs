using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip clip = null;

    public void PlayClip()
    {
        AudioManager.Instance.PlayClip(clip);
    }

}
