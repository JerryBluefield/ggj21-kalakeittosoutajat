using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : Mirror.NetworkBehaviour
{
    public static SoundPlayer Instance { get; private set; }

    private void Awake()
    {
        if (!isLocalPlayer)
        {
            return;
        }

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
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<AudioSource>().PlayOneShot(bumpSound);
    }
}
