using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffekt : MonoBehaviour
{
    public AudioSource soundPlay;
    public void PlaySound()
    {
        soundPlay.Play();
    }
}
