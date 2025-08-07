using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject popupPrefab;
    public GameObject imagePrefab;
    public GameObject parent;
    public static UIManager instance;

    public string testTitle;
    public string testDescription;
    public Sprite testImage;

    [ContextMenu("Test Notification")]
    public void TestPopup()
    {
        ShowPopupMenu(testTitle, testDescription);
    }

    [ContextMenu("Text Image Popup")]
    public void TestImagePopup()
    {
        ShowImagePopup(testTitle, testImage);
    }

    public void ShowPopupMenu(string title, string description)
    {
        GameObject popup = Instantiate(popupPrefab, parent.transform);
        PopUpControl popup_ctrl = popup.GetComponent<PopUpControl>();
        popup_ctrl.AddText(title, description);
    }

    public void ShowImagePopup(string title, Sprite image)
    {
        GameObject popup = Instantiate(imagePrefab, parent.transform);
        ImagePopUpControl popup_ctrl = popup.GetComponent<ImagePopUpControl>();
        popup_ctrl.AddText(title, image);
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
