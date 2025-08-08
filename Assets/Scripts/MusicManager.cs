using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource source;

    void Awake()
    {
      if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
         {
            Destroy(gameObject);
         }
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
