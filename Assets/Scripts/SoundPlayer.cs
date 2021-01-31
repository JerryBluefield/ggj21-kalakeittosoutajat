using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : Mirror.NetworkBehaviour
{
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
        if (bumpSound == null)
        {
            Debug.Log("bumpSound audioclip is null");
            return;
        }
        GetComponent<AudioSource>().PlayOneShot(bumpSound);
    }
}
