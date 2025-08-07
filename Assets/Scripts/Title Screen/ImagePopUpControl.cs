using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImagePopUpControl : MonoBehaviour
{
    public TMP_Text titleText;
    public Image image;
    public Button closeButton;

    public void AddText(string title, Sprite sprite)
    {
        titleText.text = title;
        image.sprite = sprite;
        closeButton.onClick.AddListener(closePopup);
        print("TimeScale = 0");
        Time.timeScale = 0;
    }

    void closePopup()
    {
        Time.timeScale = 1;
        print("TimeScale = 1");
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
