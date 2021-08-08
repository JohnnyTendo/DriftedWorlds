using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public string VolumeName;
    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(VolumeName, Mathf.Log10(sliderValue) * 20);
    }
}
