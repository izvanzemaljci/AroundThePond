using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private static AudioManager instance = null;
     
    public static AudioManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;

            s.source.loop = s.loop;
        }
    }

    private void Start() {
        Play("Theme");
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Sound: " + name + " could not be found.");
            return;
        }
        s.source.Play();
    }
}