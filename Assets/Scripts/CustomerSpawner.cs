using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{

    [Header("Customer Settings")]
    public Transform[] customerPositions;
    public Sprite[] customerSprites;
    public Customer[] instantiatedCustomers;
    public GameObject customerPrefab;
    
    [Header("Spawn Settings")]
    public float minSpawnInterval = 3f;
    public float maxSpawnInterval = 8f;
    public bool autoSpawn = true;
    
    [System.Serializable]
    public class RecipeColorSpritePair
    {
        public Recipe.RecipeColor color;
        public Sprite sprite;
    }
    
    [SerializeField]
    public RecipeColorSpritePair[] recipeColorSpritePairs;
    
    private Dictionary<Recipe.RecipeColor, Sprite> potionSprites = new Dictionary<Recipe.RecipeColor, Sprite>();
    private float nextSpawnTime;

    public static CustomerSpawner instance;

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
        
        if (autoSpawn)
        {
            SetNextSpawnTime();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (autoSpawn && Time.time >= nextSpawnTime)
        {
            SpawnCustomer();
            SetNextSpawnTime();
        }
    }
    
    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
    
    public void SpawnCustomer()
    {
        if (customerPositions.Length == 0 || customerPrefab == null)
        {
            Debug.LogWarning("No customer positions or prefab set!");
            return;
        }
        
        int index = Random.Range(0, customerPositions.Length);
        //add check for if customer is already seated at a certain position
        Transform spawnPosition = customerPositions[index];
        GameObject customerObj = Instantiate(customerPrefab, spawnPosition.position, spawnPosition.rotation);
        Customer customer = customerObj.GetComponent<Customer>();
        instantiatedCustomers[index] = customer;
        
        if (customer != null)
        {
            Recipe.RecipeColor randomColor = GetRandomRecipeColor();
            Sprite customerSprite = customerSprites[Random.Range(0, customerPositions.Length)];
            Sprite potionSprite = GetPotionSprite(randomColor);
            
            customer.Initialize(
                customerSprite, 
                potionSprite, 
                Random.Range(10f, 20f)
            );
        }
    }
    
    Recipe.RecipeColor GetRandomRecipeColor()
    {
        return RecipeController.instance.GetRandomRecipe().color;
    }
    
    Sprite GetPotionSprite(Recipe.RecipeColor color)
    {
        if (potionSprites.ContainsKey(color))
        {
            return potionSprites[color];
        }
        return null;
    }
}
