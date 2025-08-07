using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystemLv3 : MonoBehaviour
{
    public float startTime;

    List<string> tutorialTexts = new List<string>();

    public int numOfTutorials;
    public bool completed = false;
    public int index = 0;
    public int tutorialCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //print("POPOPOPOPOP");
        startTime = Time.time;
        UIManager.instance.ShowPopupMenu("Welcome!", "Welcome to Level 3!\n\nIn this level, you may use 2 different pots to cook two variant of a recipe.");
        tutorialTexts.Add("The pot on the center-left side still functions as a hot stove, and the potions produced are square bottles.\n\nThe center-right pot functions as a cold stove, producing rounded bottle versions.\n\nYou should pay attention to the variant the customer requested.");
        numOfTutorials = tutorialTexts.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            if (Time.time - startTime >= 0.001f)
            {
                startTime = Time.time;
                showNextTutorial();
            }
        }
    }

    void showNextTutorial()
    {
        if (!completed)
        {
            UIManager.instance.ShowPopupMenu("Tutorial", tutorialTexts[index]);
            index += 1;
            if (index == numOfTutorials)
            {
                completed = true;
            }
        }
    }
}
