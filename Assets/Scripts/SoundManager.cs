using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Tooltip("Order of the sound clips matters! 1. Correct 2. Wrong 3. Success")]
    [SerializeField] AudioClip[] audioClips = new AudioClip[3];     //Hard coded because we have a set amout of sound effects. Else use List

    //Enum of the different audio clips
    public enum soundClip
    {
        correct,
        wrong,
        success
    }

    public static SoundManager Instance;

    void Start()
    {
        //Singleton pattern
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySound(soundClip sfx)
    {
        AudioSource myAudio = GetComponent<AudioSource>();
        myAudio.clip = audioClips[(int)sfx];
        myAudio.Play();
    }
}
