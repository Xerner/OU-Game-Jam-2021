using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase0MusicHandler : MonoBehaviour
{
    [SerializeField]
    AudioSource intro;
    [SerializeField]
    AudioSource mainLoop;
    void Update()
    {
        if (!intro.isPlaying && !mainLoop.isPlaying)
            mainLoop.Play();
    }
}
