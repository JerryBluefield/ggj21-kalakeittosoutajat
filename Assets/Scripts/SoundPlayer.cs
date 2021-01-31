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

    [SerializeField]
    AudioClip harpoonShotSound;

    internal void PlayBumpSound()
    {
        PlaySound(bumpSound);
    }

    internal void PlayHarpoonShotSound()
    {
        PlaySound(harpoonShotSound);
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (bumpSound == null)
        {
            Debug.Log("sound: " + audioClip.name + " is null");
            return;
        }
        GetComponent<AudioSource>().PlayOneShot(audioClip);
    }
}
