using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //To access a singleton from another file: ClassName.instance.MethodName();
    public AudioClip chopSFX, bubbleSFX, splashSFX, gameOverSFX, angrySFX;
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

    // public void PlaysplashSFX(AudioClip splashSFX)
    //{
    // AudioManager.instance.PlaySFX(splashSFX);
    // }

    // public void PlaychopSFX(AudioClip chopSFX)
    // {
    //     AudioManager.instance.PlaySFX(chopSFX);
    //}

    //  public void PlaybubbleSFX(AudioClip bubbleSFX)
    // {
    //     AudioManager.instance.PlaySFX(bubbleSFX);
    // }
    //public void PlaygameOverSFX(AudioClip gameOverSFX)
    // {
    //     AudioManager.instance.PlaySFX(gameOverSFX);
    // }

    //public void PlayangrySFX(AudioClip angrySFX)
    // {
    //     AudioManager.instance.PlaySFX(angrySFX);
    // }
}