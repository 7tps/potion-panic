using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject popupPrefab;
    public GameObject parent;
    public static UIManager instance;

    public string testTitle;
    public string testDescription;

    [ContextMenu("Test Notification")]
    public void TestPopup()
    {
        ShowPopupMenu(testTitle, testDescription);
    }

    public void ShowPopupMenu(string title, string description)
    {
        GameObject popup = Instantiate(popupPrefab, parent.transform);
        PopUpControl popup_ctrl = popup.GetComponent<PopUpControl>();
        popup_ctrl.AddText(title, description);
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
