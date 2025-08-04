using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelScreenButtonControl : MonoBehaviour
{
    public Button level1Button;
    public Button returnButton;

    // Start is called before the first frame update
    void Start()
    {
        level1Button.onClick.AddListener(loadLevel1);
        returnButton.onClick.AddListener(returnToMenu);
    }

    void loadLevel1()
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
