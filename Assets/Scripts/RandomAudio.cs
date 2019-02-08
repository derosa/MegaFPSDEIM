using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    public List<AudioClip> clips;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomAudio()
    {
        int idx = UnityEngine.Random.Range(0, clips.Count);
        audioSource.PlayOneShot(clips[idx]);
    }

}
