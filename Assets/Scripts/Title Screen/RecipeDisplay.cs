using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeDisplay : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text colorText;
    public TMP_Text boilTimeText;
    public TMP_Text ingredientsText;

    public Button prevButton;
    public Button nextButton;

    private int maxIndex;
    private int curIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        DisplayRecipe(curIndex);
        maxIndex = RecipeController.instance.validRecipes.Count;
        prevButton.onClick.AddListener(loadPreviousRecipe);
        nextButton.onClick.AddListener(loadNextRecipe);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadPreviousRecipe()
    {
        if (curIndex == 0)
            curIndex = maxIndex - 1;
        else
            curIndex = curIndex - 1;
        DisplayRecipe(curIndex);
    }

    void loadNextRecipe()
    {
        if (curIndex == maxIndex - 1)
            curIndex = 0;
        else
            curIndex = curIndex + 1;
        DisplayRecipe(curIndex);
    }

    void DisplayRecipe(int index)
    {
        titleText.text = "Recipe #" + (index + 1);
        float boilTime = RecipeController.instance.validRecipes[index].boilTime;
        Recipe.RecipeColor color = RecipeController.instance.validRecipes[index].color;
        colorText.text = "Color: " + matchColorName(color);
        boilTimeText.text = "Boil Time: " + boilTime.ToString("0") + "s";

        string newText = "Ingredients Needed:\n\n";
        int totalIngredientsCount = RecipeController.instance.validRecipes[index].ingredientTypes.Count;
        for (int i = 0; i < totalIngredientsCount; i++)
        {
            RecipeController.IngredientType cur = RecipeController.instance.validRecipes[index].ingredientTypes[i];
            newText += matchIngredientName(cur);
        }
        ingredientsText.text = newText;
    }

    private string matchIngredientName(RecipeController.IngredientType cur)
    {
        switch (cur)
        {
            case RecipeController.IngredientType.emptyBottle:
                return "- Empty Bottle\n";
            case RecipeController.IngredientType.fullBottle:
                return "- Full Bottle\n";
            case RecipeController.IngredientType.avocado:
                return "- Avocado\n";
            case RecipeController.IngredientType.basil:
                return "- Basil\n";
            case RecipeController.IngredientType.garlic:
                return "- Garlic\n";
            case RecipeController.IngredientType.ginger:
                return "- Ginger\n";
            case RecipeController.IngredientType.parsnip:
                return "- Parsnip\n";
            case RecipeController.IngredientType.watermelon:
                return "- Watermelon\n";
            default:
                return null;
        }
    }

    private string matchColorName(Recipe.RecipeColor color)
    {
        switch (color)
        {
            case Recipe.RecipeColor.green:
                return "Green";
            case Recipe.RecipeColor.orange:
                return "Orange";
            case Recipe.RecipeColor.olive:
                return "Olive";
            case Recipe.RecipeColor.red:
                return "Red";
            default:
                return null;
        }
    }
}
