using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public Vector2Int gridPosition;
    public bool hasIngredient = false;
    public Ingredient heldIngredient = null;
    
    void Start()
    {
        gridPosition = new Vector2Int(
            Mathf.FloorToInt(transform.position.x + 0.5f),
            Mathf.FloorToInt(transform.position.y + 1.5f)
        );
    }

    public bool PlaceIngredient(Ingredient ingredient)
    {
        if (!hasIngredient)
        {
            heldIngredient = ingredient;
            ingredient.transform.SetParent(transform);
            ingredient.transform.localPosition = Vector3.zero;
            hasIngredient = true;
            return true;
        }
        return false;
    }

    public Ingredient RemoveIngredient()
    {
        if (hasIngredient && heldIngredient != null)
        {
            Ingredient ingredient = heldIngredient;
            heldIngredient = null;
            hasIngredient = false;
            return ingredient;
        }
        return null;
    }
} 