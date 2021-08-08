using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    [Serializable]
    public struct SoundObject
    {
        public string name;
        public AudioClip sound;
    }
    public List<SoundObject> sounds;


    private AudioSource source;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    public void CallSoundByName(string _name)
    {
        if (sounds.Exists(s => s.name == _name))
        {
            source.clip = sounds.FindLast(s => s.name == _name).sound;
            source.Play();
        }
    }
}
