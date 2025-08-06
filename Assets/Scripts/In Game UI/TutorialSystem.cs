using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        print("POPOPOPOPOP");
        UIManager.instance.ShowPopupMenu("Welcome!", "Welcome to Potion Panic! Here's a quick tutorial to get you started!\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
