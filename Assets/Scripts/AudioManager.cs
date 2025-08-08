using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //To access a singleton from another file: ClassName.instance.MethodName();
    public AudioClip chopSFX, bubbleSFX, splashSFX, gameOverSFX, angrySFX;
    private AudioSource source;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    //To call this from another file, type in AudioManager.instance.PlaySFX();

    public void PlaySFX(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlaySplashSFX()
    {
        AudioManager.instance.PlaySFX(splashSFX);
    }

    public void PlayChopSFX()
    {
        AudioManager.instance.PlaySFX(chopSFX);
    }

    public void PlayBubbleSFX()
    {
        AudioManager.instance.PlaySFX(bubbleSFX);
    }
    public void PlayGameOverSFX()
    {
        AudioManager.instance.PlaySFX(gameOverSFX);
    }

    public void PlayangrySFX(AudioClip angrySFX)
    {
        AudioManager.instance.PlaySFX(angrySFX);
    }
    
    public void VolumeOn()
    {
        source.volume = 1;
    }

    public void VolumeOff()
    {
        source.volume = 0;
    }
}