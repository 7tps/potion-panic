using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelScreenButtonControl : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button returnButton;

    public Button backButton;
    public Button playButton;

    public GameObject Menu;
    public GameObject Recipes;

    // Start is called before the first frame update
    void Start()
    {
        level2Button.onClick.AddListener(showRecipes);
        level1Button.onClick.AddListener(loadLevel1);
        playButton.onClick.AddListener(loadLevel2);
        backButton.onClick.AddListener(closeRecipes);
        returnButton.onClick.AddListener(returnToMenu);

        closeRecipes();
    }

    void showRecipes()
    {
        Menu.SetActive(false);
        Recipes.SetActive(true);
    }

    void closeRecipes()
    {
        Menu.SetActive(true);
        Recipes.SetActive(false);
    }

    void loadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    void loadLevel2()
    {
        SceneManager.LoadScene("Tavern");
    }

    void returnToMenu()
    {
        SceneManager.LoadScene("Title Screen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
