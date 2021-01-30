using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
        }
    }

    [SerializeField]
    AudioClip walkSound;

    [SerializeField]
    AudioClip bumpSound;

    internal void PlayBumpSound()
    {
        GetComponent<AudioSource>().PlayOneShot(bumpSound);
    }
}
