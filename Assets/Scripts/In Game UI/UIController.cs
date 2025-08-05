using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject canvas;

    public GameObject cutBarPrefab;
    private GameObject cutBar;

    public float cutProgress;
    public float totalCutTime;
    private Image statusBar;

    public bool initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCutProgress(RecipeController.IngredientType type, Vector3 position)
    {
        if (!initialized)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
            cutBar = Instantiate(cutBarPrefab, screenPosition, Quaternion.identity, canvas.transform);
            Transform statusTransform = cutBar.transform.Find("Status");
            statusBar = statusTransform.GetComponent<Image>();
            totalCutTime = RecipeController.instance.GetIngredientCutTime(type);
            cutProgress = totalCutTime;
            initialized = true;
        }
        cutProgress -= Time.deltaTime;
        statusBar.fillAmount = (totalCutTime-cutProgress) / totalCutTime;
        if (cutProgress <= 0)
        {
            Destroy(cutBar);
            initialized = false;
        }
    }
}
