using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public sounds[] Sounds;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (sounds s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        Play("Ambient piano");
    }

    public void Play(string name)
    {
        sounds s = Array.Find(Sounds, sounds => sounds.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }

        if (s.music)
        {
            for (int i = 0; i < Sounds.Length; i++)
            {
                if (Sounds[i].music)
                {
                    Sounds[i].source.Stop();
                }
            }
        }

        s.source.Play();
    }
}
