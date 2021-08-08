using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundQueue : MonoBehaviour
{
    public List<AudioClip> sounds;
    AudioSource source;
    int i = 0;
    bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.clip = sounds[i];
        source.Play();
        hasStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted && !source.isPlaying && !source.loop)
        {
            i++;
            source.clip = sounds[i];
            source.Play();
            if (i == sounds.Count - 1)
            {
                source.loop = true;
            }
        }
    }
}
