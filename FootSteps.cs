using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource stepSound;
    public AudioClip[] stepsAudio = new AudioClip[8];
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Step()
    {
        AudioClip clip = GetRandClip();
        stepSound.PlayOneShot(clip);
    }

    AudioClip GetRandClip()
    {
        return stepsAudio[Random.Range(0, stepsAudio.Length)];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
