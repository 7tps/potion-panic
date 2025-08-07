using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MP_LevelScreenButtonControl : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button returnButton;

    public Button recipeBookButton;

    public GameObject Menu;
    public GameObject Recipes;

    // Start is called before the first frame update
    void Start()
    {
        level1Button.onClick.AddListener(loadLevel1);
        level2Button.onClick.AddListener(loadLevel2);
        level3Button.onClick.AddListener(loadLevel3);
        recipeBookButton.onClick.AddListener(toggleRecipeScreen);
        returnButton.onClick.AddListener(returnToMenu);

        Menu.SetActive(true);
        Recipes.SetActive(false);
    }

    void toggleRecipeScreen()
    {
        Menu.SetActive(!Menu.activeInHierarchy);
        Recipes.SetActive(!Recipes.activeInHierarchy);
    }

    void loadLevel1()
    {
        SceneManager.LoadScene("MP Level 1");
    }

    void loadLevel2()
    {
        SceneManager.LoadScene("MP Level 2");
    }

    void loadLevel3()
    {
        SceneManager.LoadScene("MP Level 3");
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
