using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmVolumeManager : MonoBehaviour
{
    public void SetBGMVolume(float volume)
    {
        GetBGM.instance.SetBGMVolume(volume);
    }
}
