using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    public float startTime;

    List<string> tutorialTexts = new List<string>();

    public int numOfTutorials;
    public bool completed = false;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        print("POPOPOPOPOP");
        startTime = Time.time;
        UIManager.instance.ShowPopupMenu("Welcome!", "Welcome to Potion Panic! Here's a quick tutorial to get you started!\n");
        tutorialTexts.Add("Look below, customers will come to your potion shop very soon, they will order a potion with the specific color.");
        tutorialTexts.Add("The timer on the upper-left shows the total time you have.\n\nYou can pause the game using the button on the upper-right or Esc key");
        tutorialTexts.Add("The pause screen shows the recipes of potions that customers might order in this level.");
        numOfTutorials = tutorialTexts.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= 1.0f && !completed)
        {
            startTime = Time.time;
            UIManager.instance.ShowPopupMenu("Tutorial", tutorialTexts[index]);
            index += 1;
            if (index == numOfTutorials)
                completed = true;
        }
    }
}
