using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D rb;
    public bool movementDisabled;
    public float moveSpeed = 10;
    
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
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        
    }

    void HandleInput()
    {
        
        if (movementDisabled)
        {
            return;
        }
        
        float[] movementInput = GetMovementInput();

        Vector2 movement = new Vector2(movementInput[0], movementInput[1]).normalized;
        rb.velocity = movement * moveSpeed;
        
    }

    public float[] GetMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return new float[] { 0, 1 };
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            return new float[] { 0, -1 };
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            return new float[] { -1, 0 };
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return new float[] { 1, 0 };
        }
        else
        {
            return new float[] { 0, 0 };
        }
    }
    
}
