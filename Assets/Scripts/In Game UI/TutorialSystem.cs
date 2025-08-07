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
    public int tutorialCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        //print("POPOPOPOPOP");
        startTime = Time.time;
        UIManager.instance.ShowPopupMenu("Welcome!", "Welcome to Potion Panic! Here's a quick tutorial to get you started!\n");
        tutorialTexts.Add("Look below, customers will come to your potion shop very soon, they will order a potion with the specific color.");
        tutorialTexts.Add("The timer in the upper-left shows the total time you have.\n\nYou can pause the game using the button in the upper-right or the Esc key\n\nThe number to the left of the pause button is your current score.");
        tutorialTexts.Add("The pause screen shows all of the recipes of the potions that customers might order in this level.");
        tutorialTexts.Add("A customer just showed up! They ordered an orange potion.\n\nRemember you may check the recipes in the pause screen.");
        tutorialTexts.Add("Go to one of the crates, press Spacebar to take out an ingredient out of the crate\n\nPress Spacebar again to pick up the ingredient.");
        tutorialTexts.Add("Go in front of the pot to put the ingredient in.");
        tutorialTexts.Add("Go to the bottle cabinet to get a potion bottle.\n\nThen fill it in front of the pot.\n\nAfter filling, hand it to the customer!");
        numOfTutorials = tutorialTexts.Count;
        
        if (PlayerPrefs.GetInt("Multiplayer") == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            if (index < 3)
            {
                if (Time.time - startTime >= 0.001f)
                {
                    startTime = Time.time;
                    showNextTutorial();
                }
            }
            if (index == 3)
            {
                if (Time.time - startTime >= 1f)
                {
                    startTime = Time.time;
                    CustomerSpawner.instance.SpawnCustomerNow();
                    showNextTutorial();
                }
            }
            if (index == 4)
            {
                if (Time.time - startTime >= 2.0f)
                {
                    startTime = Time.time;
                    showNextTutorial();
                }
            }
            if (index == 5)
            {
                if (Time.time - startTime >= 3.0f)
                {
                    startTime = Time.time;
                    showNextTutorial();
                }
            }
            if (index == 6)
            {
                if (Time.time - startTime >= 12.0f)
                {
                    startTime = Time.time;
                    showNextTutorial();
                }
            }
            if (index > 6)
            {
                if (Time.time - startTime >= 2.0f)
                {
                    startTime = Time.time;
                    showNextTutorial();
                }
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
                completed = true;
        }
    }
}
