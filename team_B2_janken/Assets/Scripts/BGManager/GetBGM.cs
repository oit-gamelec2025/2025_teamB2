using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetBGM : MonoBehaviour
{
    public static GetBGM instance;

    [SerializeField] AudioSource bgmAudioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmAudioSource.volume = volume;
    }
}
