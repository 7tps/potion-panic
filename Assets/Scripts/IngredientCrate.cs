using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCrate : MonoBehaviour
{
    
    public RecipeController.Ingredients type;

    public GameObject ingredientPrefab;
    public Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnIngredient()
    {
        Instantiate(ingredientPrefab, spawnPoint.position, Quaternion.identity);
    }
}
