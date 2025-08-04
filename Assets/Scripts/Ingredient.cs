using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    public RecipeController.IngredientType type;

    public SpriteRenderer sr;
    
    public bool needToCut;
    public bool isCut = false;
    
    // Start is called before the first frame update
    void Start()
    {
        needToCut = RecipeController.instance.needToCut(type);
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = RecipeController.instance.ingredientSprites[(int)type];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
