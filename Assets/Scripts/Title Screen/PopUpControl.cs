using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpControl : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Button closeButton;

    public void AddText(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;
        closeButton.onClick.AddListener(closePopup);
        Time.timeScale = 0;
    }

    void closePopup()
    {
        Time.timeScale = 1;
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
