using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //To access a singleton from another file: ClassName.instance.MethodName();
    public AudioClip chopSFX, bubbleSFX, splashSFX;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    //To call this from another file, type in AudioManager.instance.PlaySFX();

    public void PlaySFX(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
