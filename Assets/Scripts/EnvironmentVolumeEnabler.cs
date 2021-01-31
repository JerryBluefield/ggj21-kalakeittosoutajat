using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentVolumeEnabler : MonoBehaviour
{
    private bool EnvironmentVolumeEnabled = true;

    public void EnableEnvironmentVolume()
    {
        EnvironmentVolumeEnabled = !EnvironmentVolumeEnabled;
        EnvironmentVolumeObject.Instance.gameObject.SetActive(EnvironmentVolumeEnabled);
    }
}
