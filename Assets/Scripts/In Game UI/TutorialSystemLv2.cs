using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystemLv2 : MonoBehaviour
{
    public float startTime;

    List<string> tutorialTexts = new List<string>();
    public List<Sprite> tutorialImage = new List<Sprite>();

    public int numOfTutorials;
    public bool completed = false;
    public int index = 0;
    public int tutorialCount = 0;
    public int imageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //print("POPOPOPOPOP");
        startTime = Time.time;
        UIManager.instance.ShowPopupMenu("Welcome!", "Welcome to Level 2!\n\nIn this level, the customer may order a green potion, which includes avocados that require you to cut.");
        tutorialTexts.Add("You may cut avocados by placing them onto any counter and press E, a progress bar will appear to show the progress.\n\nThen, treat the cut avocado as other ingredients and place it into the pot.");
        tutorialTexts.Add("#useImage#");
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
            if (tutorialTexts[index] == "#useImage#")
            {
                UIManager.instance.ShowImagePopup("Tutorial", tutorialImage[imageIndex]);
                index += 1;
                imageIndex += 1;
                if (index == numOfTutorials)
                {
                    completed = true;
                }
            }
            else
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
}
