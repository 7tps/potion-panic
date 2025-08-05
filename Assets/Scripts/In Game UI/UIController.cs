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
            cutBar = Instantiate(cutBarPrefab, position, Quaternion.identity, canvas.transform);
            statusBar = cutBar.GetComponentInChildren<Image>();
            totalCutTime = RecipeController.instance.GetIngredientCutTime(type);
            cutProgress = totalCutTime;
            initialized = true;
        }
        cutProgress -= Time.deltaTime;
        statusBar.fillAmount = cutProgress / totalCutTime;
        if (cutProgress <= 0)
            Destroy(gameObject);
    }
}
