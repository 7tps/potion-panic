using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    public RecipeController.Ingredients type;

    public bool needToCut;
    public bool isCut = false;
    
    // Start is called before the first frame update
    void Start()
    {
        needToCut = RecipeController.instance.needToCut(type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
