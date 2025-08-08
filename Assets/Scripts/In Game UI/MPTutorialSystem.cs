using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPTutorialSystem : MonoBehaviour
{
    public bool isLevel1 = false;

    void Start()
    {
        if (isLevel1)
        {
            UIManager.instance.ShowPopupMenu("Welcome!", "Welcome to Level 1!\n\nYou may review the tutorial in singleplayer version of Level 1. Close this window to start the game.");
        }
        else
        {
            UIManager.instance.ShowPopupMenu("Welcome!", "Welcome to Level 3!\n\nYou may review the tutorial in singleplayer version of Level 1. Close this window to start the game.");
        }
    }
}
