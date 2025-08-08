using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GetRecipeSprite : MonoBehaviour
{

    [SerializeField]
    public RecipeColorSpritePair[] recipeColorSpritePairs;

    [System.Serializable]
    public class RecipeColorSpritePair
    {
        public Recipe.RecipeColor color;
        public Sprite sprite;
    }

    private Dictionary<Recipe.RecipeColor, Sprite> potionSprites = new Dictionary<Recipe.RecipeColor, Sprite>();

    public static GetRecipeSprite instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (recipeColorSpritePairs != null)
        {
            foreach (var pair in recipeColorSpritePairs)
            {
                if (pair.sprite != null)
                {
                    potionSprites[pair.color] = pair.sprite;
                }
            }
        }
    }

    public Sprite GetPotionSprite(Recipe.RecipeColor color)
    {
        if (potionSprites.ContainsKey(color))
        {
            return potionSprites[color];
        }
        return null;
    }
}
