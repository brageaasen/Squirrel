using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Play : MonoBehaviour
{
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void PlayAttackSound()
    {
        audioManager.Play("Fox Attack");
    }
    public void PlayChaseSound()
    {
        audioManager.Play("Fox Chase");
    }
}
