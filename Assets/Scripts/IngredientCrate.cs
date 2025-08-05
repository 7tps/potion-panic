using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCrate : MonoBehaviour
{
    
    public RecipeController.IngredientType type;
    public Vector2Int gridPosition;
    public bool hasIngredient = false;

    public GameObject ingredientPrefab;
    public Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }
        
        gridPosition = new Vector2Int(
            Mathf.FloorToInt(transform.position.x + 0.5f),
            Mathf.FloorToInt(transform.position.y + 1.5f)
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnIngredient()
    {
        if (!hasIngredient)
        {
            GameObject g = Instantiate(ingredientPrefab, spawnPoint.position, Quaternion.identity);
            Ingredient i = g.GetComponent<Ingredient>();
            i.InitializeWithType(type);
            g.transform.localScale = Vector3.one;
            hasIngredient = true;
        }
    }

    public void SetHasIngredient(bool has)
    {
        hasIngredient = has;
    }
}
