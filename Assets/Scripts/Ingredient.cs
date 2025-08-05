using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    public RecipeController.IngredientType type;

    public SpriteRenderer sr;
    
    public bool needToCut;
    public bool isCut = false;
    public float cutTime = 0f;
    public float cutProgress;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Initialize()
    {
        needToCut = RecipeController.instance.needToCut(type);
        if (needToCut)
        {
            cutTime = RecipeController.instance.GetIngredientCutTime(type);
            cutProgress = cutTime;
        }
        Sprite sprite = RecipeController.instance.GetIngredientSprite(type);
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
    }
    
    public void InitializeWithType(RecipeController.IngredientType i)
    {
        Debug.Log("Initialized ingredient with type: " + i);
        type = i;
        needToCut = RecipeController.instance.needToCut(i);
        if (needToCut)
        {
            cutTime = RecipeController.instance.GetIngredientCutTime(i);
            cutProgress = cutTime;
        }
        Sprite sprite = RecipeController.instance.GetIngredientSprite(i);
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
    }
    
    public void RecipeInitialize(RecipeController.IngredientType i)
    {
        Debug.Log("Initialized ingredient with type: " + i);
        type = i;
        needToCut = RecipeController.instance.needToCut(i);
        if (needToCut)
        {
            cutTime = RecipeController.instance.GetIngredientCutTime(i);
            cutProgress = cutTime;
        }
        cutProgress = cutTime;
    }

    public void cutIngredient()
    {
        cutProgress -= Time.deltaTime;
        if (cutProgress <= 0)
        {
            cutProgress = 0;
            isCut = true;
            Sprite s =  RecipeController.instance.GetCutIngredientSprite(type);
            if (s != null)
            {
                sr.sprite = s;
            }
            return;
        }
    }
}
