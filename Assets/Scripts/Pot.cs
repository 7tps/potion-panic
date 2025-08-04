using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

    public List<Ingredient> contents;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RecipeController.instance.isValidRecipe(contents))
        {
            StartBoiling();
        }
    }

    public void AddIngredient(Ingredient i)
    {
        contents.Add(i);
        i.gameObject.SetActive(false);
    }
    
    public void StartBoiling()
    {
        
    }

    
}
