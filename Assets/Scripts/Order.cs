using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{

    public int orderIndex;
    public SpriteRenderer sr;
    public Recipe.RecipeColor color;

    public void SetSprite(Sprite s)
    {
        sr.sprite = s;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CustomerSpawner.instance.canPlayerSubmit = true;
        CustomerSpawner.instance.customerIndex = orderIndex;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        CustomerSpawner.instance.canPlayerSubmit = false;
        CustomerSpawner.instance.customerIndex = -1;
    }
}
