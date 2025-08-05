using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField]
    private float nextSpawnTime;

    [System.Serializable]
    public class RecipeColorSpritePair
    {
        public Recipe.RecipeColor color;
        public Sprite sprite;
    }
    
    [SerializeField]
    public RecipeColorSpritePair[] recipeColorSpritePairs;
    
    private Dictionary<Recipe.RecipeColor, Sprite> potionSprites = new Dictionary<Recipe.RecipeColor, Sprite>();
    
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
        
        if (instantiatedCustomers == null || instantiatedCustomers.Length != customerPositions.Length)
        {
            instantiatedCustomers = new Customer[customerPositions.Length];
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
            bool spawned = SpawnCustomer();
            SetNextSpawnTime();
            Debug.Log("Next spawn time set to: " + nextSpawnTime);
        }
    }
    
    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
    
    public bool SpawnCustomer()
    {
        if (customerPositions.Length == 0 || customerPrefab == null)
        {
            Debug.LogWarning("No customer positions or prefab set!");
            return false;
        }
        
        if (instantiatedCustomers == null || instantiatedCustomers.Length != customerPositions.Length)
        {
            Debug.LogWarning("instantiatedCustomers array not properly initialized!");
            return false;
        }
        
        int index = Random.Range(0, customerPositions.Length);
        if (index >= instantiatedCustomers.Length)
        {
            Debug.LogWarning("Index out of bounds: " + index + " >= " + instantiatedCustomers.Length);
            return false;
        }
        
        if (instantiatedCustomers[index] != null)
        {
            Debug.Log("Position " + index + " is already occupied!");
            return false;
        }
        
        Transform spawnPosition = customerPositions[index];
        GameObject customerObj = Instantiate(customerPrefab, spawnPosition.position, spawnPosition.rotation);
        Customer customer = customerObj.GetComponent<Customer>();
        instantiatedCustomers[index] = customer;
        
        if (customer != null)
        {
            Recipe.RecipeColor randomColor = GetRandomRecipeColor();
            Sprite customerSprite = customerSprites[Random.Range(0, customerSprites.Length)];
            Sprite potionSprite = GetPotionSprite(randomColor);
            
            customer.Initialize(
                customerSprite, 
                potionSprite, 
                Random.Range(10f, 20f)
            );
            return true;
        }
        return false;
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
