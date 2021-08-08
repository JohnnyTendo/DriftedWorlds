using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsaneHandler : MonoBehaviour
{
    public void InsaneEvent()
    {
        GameObject go = GameObject.FindGameObjectWithTag("FX_Music");
        go.GetComponent<AudioSource>().Play();
    }
    public void InsaneStopEvent()
    {
        GameObject go = GameObject.FindGameObjectWithTag("FX_Music");
        go.GetComponent<AudioSource>().Stop();
    }
}
